using System.ComponentModel.DataAnnotations;

namespace semester4.DTO.Genre;

public class UpdateDistrictDto
{
    [Required] public int DistrictId { get; set; }

    public string? Name { get; set; }
}