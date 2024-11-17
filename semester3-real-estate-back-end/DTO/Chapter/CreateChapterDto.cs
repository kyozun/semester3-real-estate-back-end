using System.ComponentModel.DataAnnotations;

namespace semester4.DTO.Chapter;

public class CreateChapterDto
{
    public required string Title { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
    public required int ChapterNumber { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
    public required int VolumeNumber { get; set; }

    [Required] public Guid? MangaId { get; set; }
}