using semester4.DTO.Manga;
using semester4.DTO.User;

namespace semester4.DTO.Rating;

public class RatingAndMoreDto
{
    public string Id { get; set; } = string.Empty;
    public int Score { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserDto? User { get; set; }
    public MangaDto? Manga { get; set; }
}