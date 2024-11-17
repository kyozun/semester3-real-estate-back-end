using System.ComponentModel.DataAnnotations;

namespace semester4.DTO.Comment;

public class UpdateCommentDto
{
    [MinLength(6)] public string? Name { get; set; }
}