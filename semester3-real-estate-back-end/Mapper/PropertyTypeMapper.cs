using semester3_real_estate_back_end.DTO.PropertyType;
using semester3_real_estate_back_end.Models;
using semester4.DTO.Chapter;

namespace semester3_real_estate_back_end.Mapper;

public static class PropertyTypeMapper
{
    // Convert To Dto
    public static PropertyTypeDto ConvertToPropertyTypeDto(this PropertyType propertyType)
    {
        return new PropertyTypeDto
        {
            Name = propertyType.Name,
        };
    }


    public static PropertyTypeAndMoreDto ConvertToPropertyTypeAndMoreDto(this PropertyType propertyType)
    {
        var dto = new PropertyTypeAndMoreDto
        {
            PropertyTypeId = propertyType.PropertyTypeId,
            Name = propertyType.Name,
            Properties = propertyType.Properties.Select(x => x.ConvertToPropertyDto()).ToList(),
        };

        return dto;
    }

    
    // Convert to Entity
    public static PropertyType ConvertToPropertyType(this CreatePropertyTypeDto createPropertyTypeDto)
    {
        var propertyType = new PropertyType
        {
            PropertyTypeId = Guid.NewGuid().ToString(),
            Name = createPropertyTypeDto.Name,
        };

        return propertyType;
    }
}