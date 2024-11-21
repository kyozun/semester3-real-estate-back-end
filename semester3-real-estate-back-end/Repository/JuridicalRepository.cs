using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using semester3_real_estate_back_end.Data;
using semester3_real_estate_back_end.DTO.Juridical;
using semester3_real_estate_back_end.Helpers.Query;
using semester3_real_estate_back_end.Interfaces;
using semester3_real_estate_back_end.Models;

namespace semester3_real_estate_back_end.Repository;

public class JuridicalRepository : IJuridicalRepository
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public JuridicalRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<Juridical>> GetJuridicals(JuridicalQuery juridicalQuery)
    {
        var juridicals = _context.Juridical.AsQueryable();

        // Tìm theo Name
        if (!string.IsNullOrWhiteSpace(juridicalQuery.Name))
            juridicals = juridicals.Where(x => x.Name.ToLower().Contains(juridicalQuery.Name.ToLower()));

        // Lấy thêm Include
        juridicals = juridicals.Include(x => x.Properties).ThenInclude(x => x.PropertyImages);

        return await juridicals.Skip(juridicalQuery.Offset).Take(juridicalQuery.Limit).ToListAsync();
    }


    public async Task<Juridical?> GetJuridicalById(string juridicalId)
    {
        var juridicals = _context.Juridical.AsQueryable();

        // Lấy thêm Include
        juridicals = juridicals.Include(x => x.Properties).ThenInclude(x => x.PropertyImages);

        var juridical = await juridicals.FirstOrDefaultAsync(x => x.JuridicalId == juridicalId);
        return juridical;
    }

    public async Task<HttpStatusCode> CreateJuridical(Juridical juridical)
    {
        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Kiểm tra name đã tồn tại chưa
            var existingJuridical =
                await _context.Juridical.FirstOrDefaultAsync(x => x.Name.ToLower() == juridical.Name.ToLower());

            if (existingJuridical != null)
                // Nếu đã tồn tại, trả về lỗi Conflict (409)
                return HttpStatusCode.Conflict;

            // Lưu vào database
            await _context.AddAsync(juridical);
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


    public async Task<HttpStatusCode> UpdateJuridical(UpdateJuridicalDto updateJuridicalDto)
    {
        var isAdmin = _httpContextAccessor.HttpContext!.User.IsInRole("Admin");

        // Tìm juridical theo juridicalId, nếu ko có trả về NotFound
        var juridical = await GetJuridicalById(updateJuridicalDto.JuridicalId.ToString()!);

        if (juridical == null) return HttpStatusCode.NotFound;

        if (updateJuridicalDto.Name != null)
        {
            var existingJuridical =
                await _context.Juridical.FirstOrDefaultAsync(x =>
                    x.Name.ToLower() == updateJuridicalDto.Name.ToLower());
            if (existingJuridical != null)
                // Nếu đã tồn tại, trả về lỗi Conflict (409)
                return HttpStatusCode.Conflict;
        }

        // Nếu user không phải là admin thì báo lỗi 403
        if (!isAdmin) return HttpStatusCode.Forbidden;

        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();


        // Cập nhật lại juridical
        if (updateJuridicalDto.Name != null) juridical.Name = updateJuridicalDto.Name;
        juridical.UpdatedAt = DateTime.Now;


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

    public async Task<HttpStatusCode> DeleteJuridical(List<Guid> juridicalIds)
    {
        var isAdmin = _httpContextAccessor.HttpContext!.User.IsInRole("Admin");

        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Tìm tất cả juridical theo Id
            var juridicals = await _context.Juridical
                .Where(x => juridicalIds.Select(juridicalId => juridicalId.ToString()).Contains(x.JuridicalId))
                .ToListAsync();

            // Nếu không tìm thấy
            if (juridicals.IsNullOrEmpty()) return HttpStatusCode.NotFound;

            // Nếu user không phải là admin thì báo lỗi 403
            if (!isAdmin) return HttpStatusCode.Forbidden;

            _context.Juridical.RemoveRange(juridicals);
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

    public async Task<bool> JuridicalExistsAsync(string juridicalId)
    {
        return await _context.Juridical.AnyAsync(x => x.JuridicalId == juridicalId);
    }
}