using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using semester3_real_estate_back_end.Data;
using semester3_real_estate_back_end.DTO.Ward;
using semester3_real_estate_back_end.Helpers.Query;
using semester3_real_estate_back_end.Interfaces;
using semester3_real_estate_back_end.Models;

namespace semester3_real_estate_back_end.Repository;

public class WardRepository : IWardRepository
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public WardRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<Ward>> GetWards(WardQuery wardQuery)
    {
        var wards = _context.Ward.AsQueryable();

        // Tìm theo Name
        if (!string.IsNullOrWhiteSpace(wardQuery.Name))
            wards = wards.Where(x => x.Name.ToLower().Contains(wardQuery.Name.ToLower()));

        // Tìm theo DistrictId
        if (wardQuery.DistrictId != null)
        {
            wards = wards.Where(x => x.DistrictId == wardQuery.DistrictId);
        }

        // Lấy thêm Include
        wards = wards.Include(x => x.Properties);

        return await wards.Skip(wardQuery.Offset).Take(wardQuery.Limit).ToListAsync();
    }

    public async Task<Ward?> GetWardById(int wardId)
    {
        var wards = _context.Ward.AsQueryable();

        // Lấy thêm Include
        wards = wards.Include(x => x.Properties);

        var ward = await wards.FirstOrDefaultAsync(x => x.WardId == wardId);
        return ward;
    }

    public async Task<HttpStatusCode> CreateWard(Ward ward)
    {
        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Kiểm tra name đã tồn tại chưa
            var existingWard =
                await _context.Ward.FirstOrDefaultAsync(x => x.Name.ToLower() == ward.Name.ToLower());

            if (existingWard != null)
                // Nếu đã tồn tại, trả về lỗi Conflict (409)
                return HttpStatusCode.Conflict;

            // Lưu vào database
            await _context.AddAsync(ward);
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


    public async Task<HttpStatusCode> UpdateWard(UpdateWardDto updateWardDto)
    {
        var isAdmin = _httpContextAccessor.HttpContext!.User.IsInRole("Admin");

        // Tìm ward theo wardId, nếu ko có trả về NotFound
        var ward = await GetWardById(updateWardDto.WardId);

        if (ward == null) return HttpStatusCode.NotFound;

        if (updateWardDto.Name != null)
        {
            var existingWard =
                await _context.Ward.FirstOrDefaultAsync(x =>
                    x.Name.ToLower() == updateWardDto.Name.ToLower());
            if (existingWard != null)
                // Nếu đã tồn tại, trả về lỗi Conflict (409)
                return HttpStatusCode.Conflict;
        }

        // Nếu user không phải là admin thì báo lỗi 403
        if (!isAdmin) return HttpStatusCode.Forbidden;

        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();


        // Cập nhật lại ward
        if (updateWardDto.Name != null) ward.Name = updateWardDto.Name;
        ward.UpdatedAt = DateTime.Now;


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


    public async Task<HttpStatusCode> DeleteWard(List<int> wardIds)
    {
        var isAdmin = _httpContextAccessor.HttpContext!.User.IsInRole("Admin");

        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Tìm tất cả ward theo Id
            var wards = await _context.Ward.Where(x => wardIds.Contains(x.WardId)).ToListAsync();

            // Nếu không tìm thấy
            if (wards.IsNullOrEmpty()) return HttpStatusCode.NotFound;

            // Nếu user không phải là admin thì báo lỗi 403
            if (!isAdmin) return HttpStatusCode.Forbidden;

            _context.Ward.RemoveRange(wards);
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

    public async Task<bool> WardExistsAsync(int wardId)
    {
        return await _context.Ward.AnyAsync(x => x.WardId == wardId);
    }
}