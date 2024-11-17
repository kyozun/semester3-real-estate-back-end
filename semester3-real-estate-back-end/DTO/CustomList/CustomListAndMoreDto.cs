using semester4.DTO.Manga;
using semester4.DTO.User;

namespace semester4.DTO.CustomList;

public class CustomListAndMoreDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public bool IsPublic { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserDto? User { get; set; }
    public MangaDto? Manga { get; set; }

    public List<MangaDto> Mangas { get; set; }
}