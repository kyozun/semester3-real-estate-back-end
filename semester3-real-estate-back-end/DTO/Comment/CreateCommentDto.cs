namespace semester3_real_estate_back_end.DTO.Comment;

public class CreateCommentDto
{
    // [MinLength(20)]
    public required string Name { get; set; }
    public required string PropertyId { get; set; }
}