using semester4.DTO.Author;
using semester4.DTO.ContentRating;
using semester4.DTO.CoverArt;
using semester4.DTO.Genre;

namespace semester4.DTO.Manga;

public class MangaAndMoreDto
{
    public string MangaId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Year { get; set; }

    public double Rating { get; set; }
    public int ViewCount { get; set; }

    public int LastVolume { get; set; }
    public int LastChapter { get; set; }
    public string MangaStatus { get; set; }
    public bool IsLock { get; set; }


    public List<GenreDto>? Genres { get; set; }
    public List<AuthorDto>? Authors { get; set; }
    public List<CoverArtDto>? CoverArts { get; set; }
    public List<ContentRatingDto>? ContentRatings { get; set; }
}