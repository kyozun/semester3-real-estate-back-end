using semester4.Helpers.Enums;

namespace semester4.DTO.Manga;

public class MangaDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int Year { get; set; }
    public double Rating { get; set; }
    public int ViewCount { get; set; }
    public int LastVolume { get; set; }
    public int LastChapter { get; set; }

    public bool IsLock { get; set; }
    public MangaStatus MangaStatus { get; set; }
}