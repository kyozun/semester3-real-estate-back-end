using semester4.DTO.Chapter;
using semester4.DTO.User;

namespace semester4.DTO.ChapterComment;

public class ChapterCommentAndMoreDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public UserDto? User { get; set; }
    public ChapterDto? Chapter { get; set; }
}