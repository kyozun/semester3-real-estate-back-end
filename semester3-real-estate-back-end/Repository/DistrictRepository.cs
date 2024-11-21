using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using semester3_real_estate_back_end.Data;
using semester3_real_estate_back_end.Helpers.Query;
using semester3_real_estate_back_end.Interfaces;
using semester3_real_estate_back_end.Models;
using semester4.DTO.Genre;

namespace semester3_real_estate_back_end.Repository;

public class DistrictRepository : IDistrictRepository
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DistrictRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<District>> GetDistricts(DistrictQuery districtQuery)
    {
        var districts = _context.District.AsQueryable();

        // Tìm theo Name
        if (!string.IsNullOrWhiteSpace(districtQuery.Name))
            districts = districts.Where(x => x.Name.ToLower().Contains(districtQuery.Name.ToLower()));

        // Tìm theo ProvinceId
        if (districtQuery.ProvinceId != null)
        {
            districts = districts.Where(x => x.ProvinceId == districtQuery.ProvinceId);
        }

        // Lấy thêm Include
        districts = districts.Include(x => x.Wards);

        return await districts.Skip(districtQuery.Offset).Take(districtQuery.Limit).ToListAsync();
    }

    public async Task<District?> GetDistrictById(int districtId)
    {
        var districts = _context.District.AsQueryable();

        // Lấy thêm Include
        districts = districts.Include(x => x.Wards);

        var district = await districts.FirstOrDefaultAsync(x => x.DistrictId == districtId);
        return district;
    }

    public async Task<HttpStatusCode> CreateDistrict(District district)
    {
        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Kiểm tra name đã tồn tại chưa
            var existingDistrict =
                await _context.District.FirstOrDefaultAsync(x => x.Name.ToLower() == district.Name.ToLower());

            if (existingDistrict != null)
                // Nếu đã tồn tại, trả về lỗi Conflict (409)
                return HttpStatusCode.Conflict;

            // Lưu vào database
            await _context.AddAsync(district);
            await _context.SaveChangesAsync();

            // Commit transaction nếu các dòng ở trên thành công
            await transaction.CommitAsync();
        }

        catch (Exception)
        {
            // Rollback transaction nếu có lỗi
            await transaction.RollbackAsync();
            return HttpStatusCode.BadRequest;
        }

        return HttpStatusCode.OK;
    }


    public async Task<HttpStatusCode> UpdateDistrict(UpdateDistrictDto updateDistrictDto)
    {
        var isAdmin = _httpContextAccessor.HttpContext!.User.IsInRole("Admin");

        // Tìm district theo districtId, nếu ko có trả về NotFound
        var district = await GetDistrictById(updateDistrictDto.DistrictId);

        if (district == null) return HttpStatusCode.NotFound;

        if (updateDistrictDto.Name != null)
        {
            var existingDistrict =
                await _context.District.FirstOrDefaultAsync(x =>
                    x.Name.ToLower() == updateDistrictDto.Name.ToLower());
            if (existingDistrict != null)
                // Nếu đã tồn tại, trả về lỗi Conflict (409)
                return HttpStatusCode.Conflict;
        }

        // Nếu user không phải là admin thì báo lỗi 403
        if (!isAdmin) return HttpStatusCode.Forbidden;

        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();


        // Cập nhật lại district
        if (updateDistrictDto.Name != null) district.Name = updateDistrictDto.Name;
        district.UpdatedAt = DateTime.Now;


        try
        {
            await _context.SaveChangesAsync();
            // Commit transaction nếu các dòng ở trên thành công
            await transaction.CommitAsync();
        }

        catch (Exception)
        {
            // Rollback transaction nếu có lỗi
            await transaction.RollbackAsync();
            return HttpStatusCode.BadRequest;
        }

        return HttpStatusCode.OK;
    }

    public async Task<HttpStatusCode> DeleteDistrict(List<int> districtIds)
    {
        var isAdmin = _httpContextAccessor.HttpContext!.User.IsInRole("Admin");

        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Tìm tất cả district theo Id
            var districts = await _context.District
                .Where(x => districtIds.Contains(x.DistrictId))
                .ToListAsync();

            // Nếu không tìm thấy
            if (districts.IsNullOrEmpty()) return HttpStatusCode.NotFound;

            // Nếu user không phải là admin thì báo lỗi 403
            if (!isAdmin) return HttpStatusCode.Forbidden;

            _context.District.RemoveRange(districts);
            await _context.SaveChangesAsync();
            // Commit transaction nếu các dòng ở trên thành công
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            // Rollback transaction nếu có lỗi
            await transaction.RollbackAsync();
            return HttpStatusCode.BadRequest;
        }

        return HttpStatusCode.OK;
    }

    public async Task<bool> DistrictExistsAsync(int districtId)
    {
        return await _context.District.AnyAsync(x => x.DistrictId == districtId);
    }
}