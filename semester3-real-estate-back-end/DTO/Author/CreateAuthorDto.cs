using System.ComponentModel.DataAnnotations;

namespace semester4.DTO.Author;

public class CreateAuthorDto
{
    [MinLength(6)] public required string Name { get; set; }

    [MinLength(20)] public required string Biography { get; set; }

    public string? YoutubeUrl { get; set; }
    public string? TiktokUrl { get; set; }
    public string? WebsiteUrl { get; set; }
}