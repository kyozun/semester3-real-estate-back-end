using semester3_real_estate_back_end.DTO.Property;
using semester3_real_estate_back_end.Models;

namespace semester3_real_estate_back_end.Mapper;

public static class PropertyMapper
{
    public static PropertyDto ConvertToPropertyDto(this Property property)
    {
        return new PropertyDto
        {
            Title = property.Title,
            Description = property.Description,
            ViewCount = property.ViewCount,
            Bathroom = property.Bathroom,
            Address = property.Address,
            Area = property.Area,
            Price = property.Price,
            Bedroom = property.Bedroom,
            Floor = property.Floor,
        };
    }

    public static PropertyAndMoreDto ConvertToPropertyAndMoreDto(this Property property)
    {
        var dto = new PropertyAndMoreDto
        {
            PropertyId = property.PropertyId,
            Title = property.Title,
            Description = property.Description,
            ViewCount = property.ViewCount,
            Bathroom = property.Bathroom,
            Address = property.Address,
            Area = property.Area,
            Price = property.Price,
            Bedroom = property.Bedroom,
            Floor = property.Floor,
            CoverImage = property.CoverImage,
            PropertyImages = property.PropertyImages.Select(x => x.ConvertToPropertyImageDto()).ToList(),
            Category = property.Category.ConvertToCategoryDto(),
            Direction = property.Direction.ConvertToDirectionDto(),
            Juridical = property.Juridical.ConvertToJuridicalDto(),
            PropertyType = property.PropertyType.ConvertToPropertyTypeDto(),
            Ward = property.Ward.ConvertToWardDto(),
            User = property.User.ConvertToUserDto(),
            IsExpiry = (property.ExpiryDate < DateTime.Now)
        };


        return dto;
    }

    // Convert to Entity
    public static Property ConvertToProperty(this CreatePropertyDto createPropertyDto)
    {
        return new Property
        {
            PropertyId = Guid.NewGuid().ToString(),
            Title = createPropertyDto.Title,
            Description = createPropertyDto.Description,
            Bathroom = createPropertyDto.Bathroom,
            Address = createPropertyDto.Address,
            Area = createPropertyDto.Area,
            Price = createPropertyDto.Price,
            Bedroom = createPropertyDto.Bedroom,
            Floor = createPropertyDto.Floor,
            JuridicalId = createPropertyDto.JuridicalId.ToString(),
            WardId = createPropertyDto.WardId,
            PropertyTypeId = createPropertyDto.PropertyTypeId.ToString(),
            CategoryId = createPropertyDto.CategoryId.ToString(),
            DirectionId = createPropertyDto.DirectionId.ToString(),
        };
    }
}