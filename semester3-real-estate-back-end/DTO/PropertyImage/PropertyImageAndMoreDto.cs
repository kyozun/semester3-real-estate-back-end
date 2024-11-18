
using semester3_real_estate_back_end.DTO.Property;

namespace semester3_real_estate_back_end.DTO.PropertyImage;

public class PropertyImageAndMoreDto
{
    public string PropertyImageId { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    

    public PropertyDto Property { get; set; }
}