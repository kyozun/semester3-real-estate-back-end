using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using semester3_real_estate_back_end.Data;
using semester3_real_estate_back_end.DTO.Direction;
using semester3_real_estate_back_end.Helpers.Query;
using semester3_real_estate_back_end.Interfaces;
using semester3_real_estate_back_end.Models;

namespace semester3_real_estate_back_end.Repository;

public class DirectionRepository : IDirectionRepository
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DirectionRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<Direction>> GetDirections(DirectionQuery directionQuery)
    {
        var directions = _context.Direction.AsQueryable();

        // Tìm theo Name
        if (!string.IsNullOrWhiteSpace(directionQuery.Name))
            directions = directions.Where(x => x.Name.ToLower().Contains(directionQuery.Name.ToLower()));

        // Lấy thêm Include
        directions = directions.Include(x => x.Properties).ThenInclude(x => x.PropertyImages);

        return await directions.Skip(directionQuery.Offset).Take(directionQuery.Limit).ToListAsync();
    }

    public async Task<Direction?> GetDirectionById(string directionId)
    {
        var directions = _context.Direction.AsQueryable();

        // Lấy thêm Include
        directions = directions.Include(x => x.Properties).ThenInclude(x => x.PropertyImages);

        var direction = await directions.FirstOrDefaultAsync(x => x.DirectionId == directionId);
        return direction;
    }

    public async Task<HttpStatusCode> CreateDirection(Direction direction)
    {
        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Kiểm tra name đã tồn tại chưa
            var existingDirection =
                await _context.Direction.FirstOrDefaultAsync(x => x.Name.ToLower() == direction.Name.ToLower());

            if (existingDirection != null)
                // Nếu đã tồn tại, trả về lỗi Conflict (409)
                return HttpStatusCode.Conflict;

            // Lưu vào database
            await _context.AddAsync(direction);
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


    public async Task<HttpStatusCode> UpdateDirection(UpdateDirectionDto updateDirectionDto)
    {
        var isAdmin = _httpContextAccessor.HttpContext!.User.IsInRole("Admin");

        // Tìm direction theo directionId, nếu ko có trả về NotFound
        var direction = await GetDirectionById(updateDirectionDto.DirectionId.ToString()!);

        if (direction == null) return HttpStatusCode.NotFound;

        if (updateDirectionDto.Name != null)
        {
            var existingDirection =
                await _context.Direction.FirstOrDefaultAsync(x =>
                    x.Name.ToLower() == updateDirectionDto.Name.ToLower());
            if (existingDirection != null)
                // Nếu đã tồn tại, trả về lỗi Conflict (409)
                return HttpStatusCode.Conflict;
        }

        // Nếu user không phải là admin thì báo lỗi 403
        if (!isAdmin) return HttpStatusCode.Forbidden;

        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();


        // Cập nhật lại direction
        if (updateDirectionDto.Name != null) direction.Name = updateDirectionDto.Name;
        direction.UpdatedAt = DateTime.Now;
      

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

    public async Task<HttpStatusCode> DeleteDirection(List<Guid> directionIds)
    {
        var isAdmin = _httpContextAccessor.HttpContext!.User.IsInRole("Admin");

        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Tìm tất cả direction theo Id
            var directions = await _context.Direction
                .Where(x => directionIds.Select(directionId => directionId.ToString()).Contains(x.DirectionId))
                .ToListAsync();

            // Nếu không tìm thấy
            if (directions.IsNullOrEmpty()) return HttpStatusCode.NotFound;

            // Nếu user không phải là admin thì báo lỗi 403
            if (!isAdmin) return HttpStatusCode.Forbidden;

            _context.Direction.RemoveRange(directions);
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

    public async Task<bool> DirectionExistsAsync(string directionId)
    {
        return await _context.Direction.AnyAsync(x => x.DirectionId == directionId);
    }
}