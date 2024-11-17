using semester4.DTO.ChapterComment;

namespace semester4.Mapper;

public static class ChapterChapterCommentMapper
{
    public static ChapterCommentDto ConvertToChapterCommentDto(this ChapterComment chapterComment)
    {
        return new ChapterCommentDto
        {
            Name = chapterComment.Name,
            CreatedAt = chapterComment.CreatedAt
        };
    }

    public static ChapterCommentAndMoreDto ConvertToChapterCommentAndMoreDto(this ChapterComment chapterComment)
    {
        var dto = new ChapterCommentAndMoreDto
        {
            Id = chapterComment.ChapterId,
            Name = chapterComment.Name,
            CreatedAt = chapterComment.CreatedAt
        };


        if (chapterComment.Chapter != null) dto.Chapter = chapterComment.Chapter.ConvertToChapterDto();

        if (chapterComment.User != null) dto.User = chapterComment.User.ConvertToUserDto();

        return dto;
    }

    public static ChapterComment ConvertToChapterComment(this CreateChapterCommentDto createChapterCommentDto,
        string userId)
    {
        return new ChapterComment
        {
            ChapterCommentId = Guid.NewGuid().ToString(),
            Name = createChapterCommentDto.Name,
            UserId = userId,
            ChapterId = createChapterCommentDto.ChapterId
        };
    }
}