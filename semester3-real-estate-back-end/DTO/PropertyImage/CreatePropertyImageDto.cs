using System.ComponentModel.DataAnnotations;

namespace semester4.DTO.Chapter;

public class CreatePropertyImageDto
{
    public required string ImageUrl { get; set; }
    public required string Description { get; set; }
    [Required] public Guid? PropertyId { get; set; }
}