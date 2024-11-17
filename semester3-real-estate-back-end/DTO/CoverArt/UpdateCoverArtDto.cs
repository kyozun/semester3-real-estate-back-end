using System.ComponentModel.DataAnnotations;
using semester4.Helpers.Validation;

namespace semester4.DTO.CoverArt;

public class UpdateCoverArtDto
{
    [Required] public Guid? CoverArtId { get; set; }

    [MaxFileSize(2 * 1024 * 1024)]
    [AllowedExtensions([".jpg", ".png"])]
    public IFormFile? CoverArtFile { get; set; }

    [MaxLength(1000)] public string? Description { get; set; }

    [RegularExpression("[a-z]{2}", ErrorMessage = "Phiên bản là 'en' hoặc 'jp',...")]
    public string? Locale { get; set; }

    [RegularExpression("^\\d{1,4}$", ErrorMessage = "Số tập từ 1 đến 9999")]
    public string? Volume { get; set; }
}