using System.ComponentModel.DataAnnotations;

namespace semester4.DTO.PropertyImage;

public class UpdatePropertyImageDto
{
    [Required] public Guid? PropertyImageId { get; set; }
    public string? Title { get; set; }
    public int? PropertyImageNumber { get; set; }
    public int? VolumeNumber { get; set; }
}