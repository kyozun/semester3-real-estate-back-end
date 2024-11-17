using semester4.DTO.Manga;
using semester4.DTO.User;

namespace semester4.DTO.Comment;

public class CommentAndMoreDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public UserDto? User { get; set; }
    public MangaDto? Manga { get; set; }
}