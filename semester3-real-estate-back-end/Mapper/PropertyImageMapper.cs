using semester3_real_estate_back_end.DTO.PropertyImage;
using semester3_real_estate_back_end.Models;
using semester4.DTO.Chapter;

namespace semester3_real_estate_back_end.Mapper;

public  static class PropertyImageMapper
{
    /*Convert To Dto*/
    public static PropertyImageDto ConvertToPropertyImageDto(this PropertyImage propertyImage)
    {
        return new PropertyImageDto
        {
            ImageUrl = propertyImage.ImageUrl,
            Description = propertyImage.Description,
        };
    }
    
    
    public static PropertyImageAndMoreDto ConvertToPropertyImageAndMoreDto(this PropertyImage propertyImage)
    {
        var dto = new PropertyImageAndMoreDto
        {
            PropertyImageId = propertyImage.PropertyImageId,
            ImageUrl = propertyImage.ImageUrl,
            Description = propertyImage.Description,
        };

        return dto;
    }

    /* Convert to Entity*/
    public static PropertyImage ConvertToPropertyImage(this CreatePropertyImageDto createPropertyImageDto)
    {
        return new PropertyImage
        {
            PropertyImageId = Guid.NewGuid().ToString(),
            ImageUrl = createPropertyImageDto.ImageUrl,
            Description = createPropertyImageDto.Description,
            PropertyId = createPropertyImageDto.PropertyId.ToString()!,
        };
    }
}