using semester4.DTO.Comment;

namespace semester4.Mapper;

public static class CommentMapper
{
    public static CommentDto ConvertToCommentDto(this Comment comment)
    {
        return new CommentDto
        {
            Name = comment.Name,
            CreatedAt = comment.CreatedAt
        };
    }

    public static CommentAndMoreDto ConvertToCommentAndMoreDto(this Comment comment)
    {
        var dto = new CommentAndMoreDto
        {
            Id = comment.CommentId,
            Name = comment.Name,
            CreatedAt = comment.CreatedAt
        };


        if (comment.Manga != null) dto.Manga = comment.Manga.ConvertToMangaDto();

        if (comment.User != null) dto.User = comment.User.ConvertToUserDto();

        return dto;
    }

    public static Comment ConvertToComment(this CreateCommentDto createCommentDto, string userId)
    {
        return new Comment
        {
            CommentId = Guid.NewGuid().ToString(),
            Name = createCommentDto.Name,
            MangaId = createCommentDto.MangaId,
            UserId = userId
        };
    }
}