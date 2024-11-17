namespace semester4.DTO.ChapterComment;

public class CreateChapterCommentDto
{
    // [MinLength(20)]
    public required string Name { get; set; }

    public required string ChapterId { get; set; }
}