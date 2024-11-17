using semester4.DTO.Manga;

namespace semester4.DTO.Genre;

public class GenreAndMoreDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

    public List<MangaDto>? Mangas { get; set; }
}