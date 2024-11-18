namespace semester3_real_estate_back_end.DTO.PropertyImage;

public class PropertyImageDto
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