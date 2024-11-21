using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using semester3_real_estate_back_end.Data;
using semester3_real_estate_back_end.DTO.PropertyType;
using semester3_real_estate_back_end.Helpers.Query;
using semester3_real_estate_back_end.Interfaces;
using semester3_real_estate_back_end.Models;

namespace semester3_real_estate_back_end.Repository;

public class PropertyTypeRepository : IPropertyTypeRepository
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PropertyTypeRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<PropertyType>> GetPropertyTypes(PropertyTypeQuery propertyTypeQuery)
    {
        var propertyTypes = _context.PropertyType.AsQueryable();

        // Tìm theo Name
        if (!string.IsNullOrWhiteSpace(propertyTypeQuery.Name))
            propertyTypes = propertyTypes.Where(x => x.Name.ToLower().Contains(propertyTypeQuery.Name.ToLower()));

        // Lấy thêm Include
        propertyTypes = propertyTypes.Include(x => x.Properties).ThenInclude(x => x.PropertyImages);

        return await propertyTypes.Skip(propertyTypeQuery.Offset).Take(propertyTypeQuery.Limit).ToListAsync();
    }


    public async Task<PropertyType?> GetPropertyTypeById(string propertyTypeId)
    {
        var propertyTypes = _context.PropertyType.AsQueryable();

        // Lấy thêm Include
        propertyTypes = propertyTypes.Include(x => x.Properties).ThenInclude(x => x.PropertyImages);

        var propertyType = await propertyTypes.FirstOrDefaultAsync(x => x.PropertyTypeId == propertyTypeId);
        return propertyType;
    }

    public async Task<HttpStatusCode> CreatePropertyType(PropertyType propertyType)
    {
        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Kiểm tra name đã tồn tại chưa
            var existingPropertyType =
                await _context.PropertyType.FirstOrDefaultAsync(x => x.Name.ToLower() == propertyType.Name.ToLower());

            if (existingPropertyType != null)
                // Nếu đã tồn tại, trả về lỗi Conflict (409)
                return HttpStatusCode.Conflict;

            // Lưu vào database
            await _context.AddAsync(propertyType);
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


    public async Task<HttpStatusCode> UpdatePropertyType(UpdatePropertyTypeDto updatePropertyTypeDto)
    {
        var isAdmin = _httpContextAccessor.HttpContext!.User.IsInRole("Admin");

        // Tìm propertyType theo propertyTypeId, nếu ko có trả về NotFound
        var propertyType = await GetPropertyTypeById(updatePropertyTypeDto.PropertyTypeId.ToString()!);

        if (propertyType == null) return HttpStatusCode.NotFound;

        if (updatePropertyTypeDto.Name != null)
        {
            var existingPropertyType =
                await _context.PropertyType.FirstOrDefaultAsync(x =>
                    x.Name.ToLower() == updatePropertyTypeDto.Name.ToLower());
            if (existingPropertyType != null)
                // Nếu đã tồn tại, trả về lỗi Conflict (409)
                return HttpStatusCode.Conflict;
        }

        // Nếu user không phải là admin thì báo lỗi 403
        if (!isAdmin) return HttpStatusCode.Forbidden;

        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();


        // Cập nhật lại propertyType
        if (updatePropertyTypeDto.Name != null) propertyType.Name = updatePropertyTypeDto.Name;
        propertyType.UpdatedAt = DateTime.Now;


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

    public async Task<HttpStatusCode> DeletePropertyType(List<Guid> propertyTypeIds)
    {
        var isAdmin = _httpContextAccessor.HttpContext!.User.IsInRole("Admin");

        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Tìm tất cả propertyType theo Id
            var propertyTypes = await _context.PropertyType
                .Where(x => propertyTypeIds.Select(propertyTypeId => propertyTypeId.ToString())
                    .Contains(x.PropertyTypeId))
                .ToListAsync();

            // Nếu không tìm thấy
            if (propertyTypes.IsNullOrEmpty()) return HttpStatusCode.NotFound;

            // Nếu user không phải là admin thì báo lỗi 403
            if (!isAdmin) return HttpStatusCode.Forbidden;

            _context.PropertyType.RemoveRange(propertyTypes);
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

    public async Task<bool> PropertyTypeExistsAsync(string propertyTypeId)
    {
        return await _context.PropertyType.AnyAsync(x => x.PropertyTypeId == propertyTypeId);
    }
}