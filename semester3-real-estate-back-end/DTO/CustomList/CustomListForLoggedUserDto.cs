using semester4.DTO.Manga;

namespace semester4.DTO.CustomList;

public class CustomListForLoggedUserDto
{
    public string Name { get; set; } = string.Empty;

    public bool IsPublic { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<MangaDto> Mangas { get; set; }
}