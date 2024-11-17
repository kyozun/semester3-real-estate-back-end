namespace semester4.DTO.Chapter;

public class ChapterDto
{
    public string? Title { get; set; }
    public int ChapterNumber { get; set; }
    public int? VolumeNumber { get; set; }

    public int ViewCount { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime PublishAt { get; set; }
    public DateTime ReadableAt { get; set; }
}