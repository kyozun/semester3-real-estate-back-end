using System.ComponentModel.DataAnnotations;
using semester4.Helpers.Enums;

namespace semester4.DTO.Manga;

public class UpdatePropertyDto
{
    [Required] public string Title { get; set; }
    [Required] public string Description { get; set; }
    [Required] public string CoverImage { get; set; }
    [Required] public string Address { get; set; }
    [Required] public double Price { get; set; }
    [Required] public string Furniture { get; set; }
    [Required] public double Area { get; set; }
    [Required] public int Floor { get; set; }
    [Required] public int Bedroom { get; set; }
    [Required] public int Bathroom { get; set; }
}