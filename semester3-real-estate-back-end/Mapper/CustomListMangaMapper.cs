using semester4.DTO.CustomListManga;

namespace semester4.Mapper;

public static class CustomListMangaMangaMapper
{
    public static CustomListManga ConvertToCustomListManga(this CreateCustomListMangaDto createCustomListMangaDto)
    {
        return new CustomListManga
        {
            CustomListId = createCustomListMangaDto.CustomListId
        };
    }
}