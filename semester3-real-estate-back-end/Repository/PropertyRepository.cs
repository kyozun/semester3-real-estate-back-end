using System.Net;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using semester3_real_estate_back_end.Data;
using semester3_real_estate_back_end.DTO.Property;
using semester3_real_estate_back_end.Helpers.Query;
using semester3_real_estate_back_end.Interfaces;
using semester3_real_estate_back_end.Models;

namespace semester3_real_estate_back_end.Repository;

public class PropertyRepository : IPropertyRepository
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PropertyRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<Property>> GetProperties(PropertyQuery propertyQuery)
    {
        var properties = _context.Property.AsQueryable();

        // Tìm theo Title
        if (!string.IsNullOrWhiteSpace(propertyQuery.Title))
            properties = properties.Where(x => x.Title.ToLower().Contains(propertyQuery.Title.ToLower()));


        // Tìm theo Price
        if (!string.IsNullOrWhiteSpace(propertyQuery.Price))
        {
            var minPrice = double.Parse(propertyQuery.Price.Split("-")[0]);
            var maxPrice = double.Parse(propertyQuery.Price.Split("-")[1]);
            properties = properties.Where(x => minPrice <= x.Price && x.Price <= maxPrice);
        }

        // Tìm theo Floor
        if (propertyQuery.Floor is > 0)
        {
            properties = properties.Where(x => x.Floor == propertyQuery.Floor);
        }

        // Tìm theo Bedroom
        if (propertyQuery.Bedroom is > 0)
        {
            properties = properties.Where(x => x.Bedroom == propertyQuery.Bedroom);
        }

        // Tìm theo Bathroom
        if (propertyQuery.Bathroom is > 0)
        {
            properties = properties.Where(x => x.Bathroom == propertyQuery.Bathroom);
        }

        // Tìm theo Area
        if (!string.IsNullOrWhiteSpace(propertyQuery.Area))
        {
            var minArea = double.Parse(propertyQuery.Area.Split("-")[0]);
            var maxArea = double.Parse(propertyQuery.Area.Split("-")[1]);
            properties = properties.Where(x => minArea <= x.Area && x.Area <= maxArea);
        }


        // Tìm theo PropertyIds
        if (!propertyQuery.PropertyIds.IsNullOrEmpty())
        {
            properties = properties.Where(x =>
                propertyQuery.PropertyIds!.Select(y => y.ToString()).Contains(x.PropertyId));
        }

        // Tìm theo UserIds
        if (!propertyQuery.UserIds.IsNullOrEmpty())
        {
            properties = properties.Where(x =>
                propertyQuery.UserIds!.Select(y => y.ToString()).Contains(x.UserId));
        }

        // Tìm theo DirectionIds
        if (!propertyQuery.DirectionIds.IsNullOrEmpty())
        {
            properties = properties.Where(x =>
                propertyQuery.DirectionIds!.Select(y => y.ToString()).Contains(x.DirectionId));
        }

        // Tìm theo PropertyTypeIds
        if (!propertyQuery.PropertyTypeIds.IsNullOrEmpty())
        {
            properties = properties.Where(x =>
                propertyQuery.PropertyTypeIds!.Select(y => y.ToString()).Contains(x.PropertyTypeId));
        }

        // Tìm theo CategoryIds
        if (!propertyQuery.CategoryIds.IsNullOrEmpty())
        {
            properties = properties.Where(x =>
                propertyQuery.CategoryIds!.Select(y => y.ToString()).Contains(x.CategoryId));
        }

        // Tìm theo JuridicalIds
        if (!propertyQuery.JuridicalIds.IsNullOrEmpty())
        {
            properties = properties.Where(x =>
                propertyQuery.JuridicalIds!.Select(y => y.ToString()).Contains(x.JuridicalId));
        }

        // Tìm theo WardId
        if (propertyQuery.WardId != null && propertyQuery.WardId != Guid.Empty)
        {
            properties = properties.Where(x =>
                propertyQuery.JuridicalIds!.Select(y => y.ToString()).Contains(x.JuridicalId));
        }


        // Include
        properties = properties.Include(x => x.Ward).Include(x => x.Juridical).Include(x => x.Category)
            .Include(x => x.PropertyType).Include(x => x.Direction).Include(x => x.PropertyImages).Include(x => x.User);


        return await properties.Skip(propertyQuery.Offset).Take(propertyQuery.Limit).ToListAsync();
    }


    public async Task<Property?> GetPropertyById(string propertyId)
    {
        var properties = _context.Property.AsQueryable();

        // Include
        properties = properties.Include(x => x.Ward).Include(x => x.Juridical).Include(x => x.Category)
            .Include(x => x.PropertyType).Include(x => x.Direction).Include(x => x.PropertyImages).Include(x => x.User);

        var property = await properties.FirstOrDefaultAsync(x => x.PropertyId == propertyId);
        return property;
    }

    public async Task<HttpStatusCode> CreateProperty(Property property, List<IFormFile> images)
    {
        var userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        property.UserId = userId;

        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Kiểm tra name đã tồn tại chưa
            var existingProperty =
                await _context.Property.FirstOrDefaultAsync(x => x.Title.ToLower() == property.Title.ToLower());

            if (existingProperty != null)
                // Nếu đã tồn tại, trả về lỗi Conflict (409)
                return HttpStatusCode.Conflict;

            // Lưu vào database
            await _context.AddAsync(property);
            await _context.SaveChangesAsync();


            foreach (var image in images)
            {
                if (image.Length > 0)
                {
                    var imageUrl = await SaveImageAsync(image);

                    var propertyImage = new PropertyImage
                    {
                        PropertyImageId = Guid.NewGuid().ToString(),
                        PropertyId = property.PropertyId,
                        Description = "Description",
                        ImageUrl = imageUrl
                    };

                    await _context.AddAsync(propertyImage);
                }
            }

            // Lưu vào database
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


    public async Task<HttpStatusCode> UpdateProperty(UpdatePropertyDto updatePropertyDto)
    {
        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            // var isAdmin = _httpContextAccessor.HttpContext!.User.IsInRole("Admin");

            // Tìm property theo propertyId, nếu ko có trả về NotFound
            var existingProperty = await _context.Property.Include(property => property.PropertyImages)
                .FirstOrDefaultAsync(p => p.PropertyId == updatePropertyDto.PropertyId.ToString());

            if (existingProperty == null) return HttpStatusCode.NotFound;


            // Xóa ảnh trong db
            if (!updatePropertyDto.ImagesToRemove.IsNullOrEmpty())
            {
                foreach (var imageUrl in updatePropertyDto.ImagesToRemove!)
                {
                    var imageToRemove = existingProperty.PropertyImages.FirstOrDefault(x => x.ImageUrl == imageUrl);
                    if (imageToRemove != null)
                    {
                        _context.Remove(imageToRemove);
                        // Delete image from storage
                        DeleteImageFromStorage(imageUrl);
                    }
                }
            }

            // Thêm ảnh mới
            if (updatePropertyDto.NewImages != null && updatePropertyDto.NewImages.Count > 0)
            {
                foreach (var image in updatePropertyDto.NewImages)
                {
                    if (image.Length > 0)
                    {
                        var imageUrl = await SaveImageAsync(image);

                        var propertyImage = new PropertyImage
                        {
                            PropertyImageId = Guid.NewGuid().ToString(),
                            PropertyId = existingProperty.PropertyId,
                            Description = "Description", 
                            ImageUrl = imageUrl
                        };

                        _context.PropertyImage.Add(propertyImage);
                    }
                }
            }


            // Nếu user không phải là admin thì báo lỗi 403
            // if (!isAdmin) return HttpStatusCode.Forbidden;

            // Cập nhật lại property
            existingProperty.Title = updatePropertyDto.Title;
            existingProperty.Description = updatePropertyDto.Description;
            existingProperty.Address = updatePropertyDto.Address;
            existingProperty.Price = updatePropertyDto.Price;
            existingProperty.Area = updatePropertyDto.Area;
            existingProperty.Floor = updatePropertyDto.Floor;
            existingProperty.Floor = updatePropertyDto.Floor;
            existingProperty.Bedroom = updatePropertyDto.Bedroom;
            existingProperty.Bedroom = updatePropertyDto.Bedroom;
            existingProperty.Bathroom = updatePropertyDto.Bathroom;
            existingProperty.UpdatedAt = DateTime.Now;
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

    public async Task<HttpStatusCode> DeleteProperty(List<Guid> propertyIds)
    {
        var isAdmin = _httpContextAccessor.HttpContext!.User.IsInRole("Admin");

        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Tìm tất cả property theo Id
            var properties = await _context.Property
                .Where(x => propertyIds.Select(propertyId => propertyId.ToString()).Contains(x.PropertyId))
                .ToListAsync();

            // Nếu không tìm thấy
            if (properties.IsNullOrEmpty()) return HttpStatusCode.NotFound;

            // Nếu user không phải là admin thì báo lỗi 403
            if (!isAdmin) return HttpStatusCode.Forbidden;

            _context.Property.RemoveRange(properties);
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

    public async Task<bool> PropertyExistsAsync(string propertyId)
    {
        return await _context.Property.AnyAsync(x => x.PropertyId == propertyId);
    }

    public async Task<string> SaveImageAsync(IFormFile image)
    {
        var uniqueFileName = Guid.NewGuid() + "_" + image.FileName;
        var imagePath = Path.Combine("wwwroot", "images", uniqueFileName);

        await using (var stream = new FileStream(imagePath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }

        var imageUrl = $"/images/{uniqueFileName}";
        return imageUrl;
    }

    private void DeleteImageFromStorage(string imageUrl)
    {
        if (string.IsNullOrEmpty(imageUrl)) return;

        var imagePath = Path.Combine("wwwroot", imageUrl.TrimStart('/'));
        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }
    }
}