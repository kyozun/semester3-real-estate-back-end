using semester4.DTO.Manga;
using semester4.DTO.UserManga;
using semester4.DTO.UserMangaFollow;

namespace semester4.Mapper;

public static class UserMangaMapper
{
    public static UserMangaAndMoreDto ConvertToUserMangaAndMoreDto(this UserManga userManga)
    {
        var dto = new UserMangaAndMoreDto
        {
            Id = userManga.UserMangaId,
            CreatedAt = userManga.CreatedAt
        };


        if (userManga.Manga != null) dto.Manga = userManga.Manga.ConvertToMangaDto();

        if (userManga.User != null) dto.User = userManga.User.ConvertToUserDto();

        return dto;
    }

    public static MangaAndMoreDto ConvertToMangaAndMoreDto(this UserManga userManga)
    {
        var dto = new MangaAndMoreDto();
        if (userManga.Manga != null)
        {
            dto.MangaId = userManga.Manga.MangaId;
            dto.Title = userManga.Manga.Title;
            dto.Description = userManga.Manga.Description;
            dto.Year = userManga.Manga.Year;
            dto.Rating = userManga.Manga.Ratings.Select(x => x.Score).DefaultIfEmpty(0).Average();
            dto.ViewCount = userManga.Manga.ViewCount;
            dto.MangaStatus = userManga.Manga.MangaStatus.ToString();
            dto.LastVolume = userManga.Manga.Chapters.Select(x => x.ChapterNumber).OrderByDescending(x => x)
                .FirstOrDefault(0);
            dto.LastChapter = userManga.Manga.Chapters.Select(x => x.VolumeNumber).OrderByDescending(x => x)
                .FirstOrDefault(0);
            dto.IsLock = userManga.Manga.IsLock;
        }

        return dto;
    }

    public static UserManga ConvertToUserManga(this CreateUserMangaDto createUserMangaDto)
    {
        return new UserManga
        {
            UserMangaId = Guid.NewGuid().ToString(),
            UserId = createUserMangaDto.UserId,
            MangaId = createUserMangaDto.MangaId
        };
    }

    public static MangaReadingStatusDto ConvertToMangaReadingStatusDto(this UserManga userManga)
    {
        return new MangaReadingStatusDto
        {
            MangaId = userManga.MangaId,
            MangaReadingStatus = userManga.MangaReadingStatus.ToString()
        };
    }
}