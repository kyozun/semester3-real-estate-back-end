using semester3_real_estate_back_end.DTO.PropertyImage;
using semester4.DTO.Chapter;

namespace semester4.Mapper;

public static class ChapterMapper
{
    public static PropertyImageDto? ConvertToChapterDto(this Chapter? chapter)
    {
        if (chapter == null) return null;
        return new PropertyImageDto
        {
            Title = chapter.Title,
            ChapterNumber = chapter.ChapterNumber,
            VolumeNumber = chapter.VolumeNumber,
            ViewCount = chapter.ViewCount,
            CreatedAt = chapter.CreatedAt,
            UpdatedAt = chapter.UpdatedAt,
            PublishAt = chapter.PublishAt,
            ReadableAt = chapter.ReadableAt
        };
    }

    public static PropertyImageAndMoreDto ConvertToChapterAndMoreDto(this Chapter chapter)
    {
        var dto = new PropertyImageAndMoreDto
        {
            ChapterId = chapter.ChapterId,
            Title = chapter.Title,
            ChapterNumber = chapter.ChapterNumber,
            VolumeNumber = chapter.VolumeNumber,
            ViewCount = chapter.ViewCount,
            CreatedAt = chapter.CreatedAt,
            UpdatedAt = chapter.UpdatedAt,
            PublishAt = chapter.PublishAt,
            ReadableAt = chapter.ReadableAt
        };

        if (chapter.Manga != null) dto.Manga = chapter.Manga.ConvertToMangaDto();
        if (chapter.User != null) dto.User = chapter.User.ConvertToUserDto();

        return dto;
    }

    public static Chapter ConvertToChapter(this CreatePropertyImageDto createPropertyImageDto)
    {
        return new Chapter
        {
            ChapterId = Guid.NewGuid().ToString(),
            Title = createPropertyImageDto.Title,
            ChapterNumber = createPropertyImageDto.ChapterNumber,
            VolumeNumber = createPropertyImageDto.VolumeNumber,
            MangaId = createPropertyImageDto.MangaId.ToString()!
        };
    }
}