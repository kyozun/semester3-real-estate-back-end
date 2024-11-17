using System.ComponentModel.DataAnnotations;

namespace semester4.DTO.City;

public class UpdateCityDto
{
    [Required] public string Name { get; set; } = string.Empty;
}