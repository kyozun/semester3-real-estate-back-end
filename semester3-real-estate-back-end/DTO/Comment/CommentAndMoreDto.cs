using semester3_real_estate_back_end.DTO.Property;
using semester3_real_estate_back_end.DTO.User;

namespace semester3_real_estate_back_end.DTO.Comment;

public class CommentAndMoreDto
{
    public string CommentId { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserDto? User { get; set; }
    public PropertyDto? Property { get; set; }
}