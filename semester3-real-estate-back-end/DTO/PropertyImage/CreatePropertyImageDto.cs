using System.ComponentModel.DataAnnotations;

namespace semester3_real_estate_back_end.DTO.PropertyImage;

public class CreatePropertyImageDto
{
    [Required] public Guid? PropertyId { get; set; }
    public required string ImageUrl { get; set; }
    public required string Description { get; set; }
  
}