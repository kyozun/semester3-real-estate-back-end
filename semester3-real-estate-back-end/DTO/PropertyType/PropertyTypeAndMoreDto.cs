
using semester3_real_estate_back_end.DTO.Property;

namespace semester3_real_estate_back_end.DTO.PropertyType;

public class PropertyTypeAndMoreDto
{
    public string PropertyTypeId { get; set; }
    public string Name { get; set; }

    public List<PropertyDto> Properties { get; set; }
}