using System.ComponentModel.DataAnnotations;

namespace semester4.DTO.Chapter;

public class UpdateChapterDto
{
    [Required] public Guid? ChapterId { get; set; }
    public string? Title { get; set; }
    public int? ChapterNumber { get; set; }
    public int? VolumeNumber { get; set; }
}