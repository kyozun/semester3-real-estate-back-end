using System.ComponentModel.DataAnnotations;
using semester4.Helpers.Enums;

namespace semester4.DTO.Manga;

public class CreateMangaDto
{
    [Required] public string Title { get; set; }
    [Required] public string Description { get; set; }
    [Required] public int Year { get; set; }

    [EnumDataType(typeof(MangaStatus), ErrorMessage = "MangaStatus must be between 1 and 2")]
    public required MangaStatus MangaStatus { get; set; }

    // ID
    [Required] public List<Guid> GenreIds { get; set; }
    [Required] public List<Guid> AuthorIds { get; set; }
    [Required] public List<Guid> ContentRatingIds { get; set; }
}