using semester3_real_estate_back_end.DTO.Property;
using semester4.DTO.Manga;

namespace semester3_real_estate_back_end.DTO.Category;

public class CategoryAndMoreDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Biography { get; set; } = string.Empty;
    public string YoutubeUrl { get; set; } = string.Empty;
    public string TiktokUrl { get; set; } = string.Empty;
    public string WebsiteUrl { get; set; } = string.Empty;

    public List<PropertyDto> Properties { get; set; }
}