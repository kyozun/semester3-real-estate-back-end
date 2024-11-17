using semester4.DTO.CustomList;

namespace semester4.Mapper;

public static class CustomListMapper
{
    public static CustomListDto ConvertToCustomListDto(this CustomList customList)
    {
        return new CustomListDto
        {
            Name = customList.Name,
            IsPublic = customList.IsPublic,
            CreatedAt = customList.CreatedAt
        };
    }

    public static CustomListAndMoreDto ConvertToCustomListAndMoreDto(this CustomList customList)
    {
        var dto = new CustomListAndMoreDto
        {
            Id = customList.CustomListId,
            Name = customList.Name,
            IsPublic = customList.IsPublic,
            CreatedAt = customList.CreatedAt
        };

        if (!customList.CustomListMangas.IsNullOrEmpty())
            dto.Mangas = customList.CustomListMangas.Select(x => x.Manga.ConvertToMangaDto()).ToList();


        if (customList.User != null) dto.User = customList.User.ConvertToUserDto();

        return dto;
    }

    public static CustomList ConvertToCustomList(this CreateCustomListDto createCustomListDto)
    {
        return new CustomList
        {
            CustomListId = Guid.NewGuid().ToString(),
            Name = createCustomListDto.Name,
            IsPublic = createCustomListDto.IsPublic
        };
    }
}