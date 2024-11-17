namespace semester4.DTO.Comment;

public class CreateCommentDto
{
    // [MinLength(20)]
    public required string Name { get; set; }
    public required string MangaId { get; set; }
}