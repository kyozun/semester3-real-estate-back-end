using System.ComponentModel.DataAnnotations;

namespace semester4.DTO.Genre;

public class UpdateDistrictDto
{
    [Required] public Guid? GenreId { get; set; }

    public string? Name { get; set; }
}