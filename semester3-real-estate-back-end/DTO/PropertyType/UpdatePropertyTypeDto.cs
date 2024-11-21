using System.ComponentModel.DataAnnotations;

namespace semester3_real_estate_back_end.DTO.PropertyType;

public class UpdatePropertyTypeDto
{
    [Required] public Guid? PropertyTypeId { get; set; }
    public string Name { get; set; }
}