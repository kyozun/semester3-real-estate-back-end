using System.ComponentModel.DataAnnotations;
using semester4.Helpers.Enums;

namespace semester4.DTO.Manga;

public class UpdateMangaDto
{
    [Required] public Guid? MangaId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? Year { get; set; }

    [EnumDataType(typeof(MangaStatus), ErrorMessage = "MangaStatus must be between 1 and 2")]
    public MangaStatus? MangaStatus { get; set; }

    // ID
    public List<Guid>? GenreIds { get; set; }
    // public List<Guid>? AuthorIds { get; set; }
    // public List<Guid>? ContentRatingIds { get; set; }
}