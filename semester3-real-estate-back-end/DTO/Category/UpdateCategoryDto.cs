using Microsoft.Build.Framework;

namespace semester4.DTO.Author;

public class UpdateCategoryDto
{
    [Required] public Guid? AuthorId { get; set; }
    public string? Name { get; set; }
    public string? Biography { get; set; }
    public string? YoutubeUrl { get; set; }
    public string? TiktokUrl { get; set; }
    public string? WebsiteUrl { get; set; }
}