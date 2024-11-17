using System.ComponentModel.DataAnnotations;
using semester4.Helpers.Validation;

namespace semester4.DTO.CoverArt;

public class CreateCoverArtDto
{
    [Required]
    [MaxFileSize(2 * 1024 * 1024)]
    [AllowedExtensions([".jpg", ".png"])]
    public required IFormFile CoverArtFile { get; set; }

    [Required] [MaxLength(1000)] public required string Description { get; set; }


    [Required]
    [RegularExpression("[a-z]{2}", ErrorMessage = "Phiên bản là 'en' hoặc 'jp',...")]
    public string Locale { get; set; } = string.Empty;


    [Required]
    [RegularExpression("^\\d{1,4}$", ErrorMessage = "Số tập từ 1 đến 9999")]
    public string Volume { get; set; } = string.Empty;

    [Required] public Guid? MangaId { get; set; }
}