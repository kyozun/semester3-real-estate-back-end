using semester3_real_estate_back_end.DTO.PropertyImage;
using semester3_real_estate_back_end.DTO.User;
using semester4.DTO.Chapter;

namespace semester4.DTO.ChapterComment;

public class ChapterCommentAndMoreDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public UserDto? User { get; set; }
    public PropertyImageDto? Chapter { get; set; }
}