using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using semester3_real_estate_back_end.Data;
using semester3_real_estate_back_end.DTO.Province;
using semester3_real_estate_back_end.Helpers.Query;
using semester3_real_estate_back_end.Interfaces;
using semester3_real_estate_back_end.Models;

namespace semester3_real_estate_back_end.Repository;

public class ProvinceRepository : IProvinceRepository
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ProvinceRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<Province>> GetProvinces(ProvinceQuery provinceQuery)
    {
        var provinces = _context.Province.AsQueryable();

        // Tìm theo Name
        if (!string.IsNullOrWhiteSpace(provinceQuery.Name))
            provinces = provinces.Where(x => x.Name.ToLower().Contains(provinceQuery.Name.ToLower()));

        // Lấy thêm Include
        provinces = provinces.Include(x => x.Districts);

        return await provinces.Skip(provinceQuery.Offset).Take(provinceQuery.Limit).ToListAsync();
    }

    public async Task<Province?> GetProvinceById(int provinceId)
    {
        var provinces = _context.Province.AsQueryable();

        // Lấy thêm Include
        provinces = provinces.Include(x => x.Districts);

        var province = await provinces.FirstOrDefaultAsync(x => x.ProvinceId == provinceId);
        return province;
    }

    public async Task<HttpStatusCode> CreateProvince(Province province)
    {
        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Kiểm tra name đã tồn tại chưa
            var existingProvince =
                await _context.Province.FirstOrDefaultAsync(x => x.Name.ToLower() == province.Name.ToLower());

            if (existingProvince != null)
                // Nếu đã tồn tại, trả về lỗi Conflict (409)
                return HttpStatusCode.Conflict;

            // Lưu vào database
            await _context.AddAsync(province);
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


    public async Task<HttpStatusCode> UpdateProvince(UpdateProvinceDto updateProvinceDto)
    {
        var isAdmin = _httpContextAccessor.HttpContext!.User.IsInRole("Admin");

        // Tìm province theo provinceId, nếu ko có trả về NotFound
        var province = await GetProvinceById(updateProvinceDto.ProvinceId);

        if (province == null) return HttpStatusCode.NotFound;

        if (updateProvinceDto.Name != null)
        {
            var existingProvince =
                await _context.Province.FirstOrDefaultAsync(x =>
                    x.Name.ToLower() == updateProvinceDto.Name.ToLower());
            if (existingProvince != null)
                // Nếu đã tồn tại, trả về lỗi Conflict (409)
                return HttpStatusCode.Conflict;
        }

        // Nếu user không phải là admin thì báo lỗi 403
        if (!isAdmin) return HttpStatusCode.Forbidden;

        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();


        // Cập nhật lại province
        if (updateProvinceDto.Name != null) province.Name = updateProvinceDto.Name;
        province.UpdatedAt = DateTime.Now;


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

    public async Task<HttpStatusCode> DeleteProvince(List<int> provinceIds)
    {
        var isAdmin = _httpContextAccessor.HttpContext!.User.IsInRole("Admin");

        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Tìm tất cả province theo Id
            var provinces = await _context.Province
                .Where(x => provinceIds.Contains(x.ProvinceId))
                .ToListAsync();

            // Nếu không tìm thấy
            if (provinces.IsNullOrEmpty()) return HttpStatusCode.NotFound;

            // Nếu user không phải là admin thì báo lỗi 403
            if (!isAdmin) return HttpStatusCode.Forbidden;

            _context.Province.RemoveRange(provinces);
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

    public async Task<bool> ProvinceExistsAsync(int provinceId)
    {
        return await _context.Province.AnyAsync(x => x.ProvinceId == provinceId);
    }
}