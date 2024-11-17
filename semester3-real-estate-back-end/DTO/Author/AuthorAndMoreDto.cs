using semester4.DTO.Manga;

namespace semester4.DTO.Author;

public class AuthorAndMoreDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Biography { get; set; } = string.Empty;
    public string YoutubeUrl { get; set; } = string.Empty;
    public string TiktokUrl { get; set; } = string.Empty;
    public string WebsiteUrl { get; set; } = string.Empty;

    public List<MangaDto?> Mangas { get; set; }
}