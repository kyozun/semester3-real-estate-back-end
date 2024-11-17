using semester4.DTO.Manga;
using semester4.DTO.User;

namespace semester4.DTO.Chapter;

public class ChapterAndMoreDto
{
    public string ChapterId { get; set; } = string.Empty;
    public string? Title { get; set; }
    public int ChapterNumber { get; set; }
    public int VolumeNumber { get; set; }

    public int ViewCount { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime PublishAt { get; set; }
    public DateTime ReadableAt { get; set; }

    public MangaDto? Manga { get; set; }
    public UserDto? User { get; set; }
}