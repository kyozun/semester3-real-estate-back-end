using semester4.DTO.Manga;
using semester4.DTO.User;

namespace semester4.DTO.CoverArt;

public class CoverArtAndMoreDto
{
    public string CoverArtId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Volume { get; set; } = string.Empty;
    public string Locale { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public MangaDto? Manga { get; set; }
    public UserDto? User { get; set; }
}