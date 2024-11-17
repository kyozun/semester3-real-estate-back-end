using semester4.DTO.Chapter;

namespace semester4.Mapper;

public static class ChapterMapper
{
    public static ChapterDto? ConvertToChapterDto(this Chapter? chapter)
    {
        if (chapter == null) return null;
        return new ChapterDto
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

    public static ChapterAndMoreDto ConvertToChapterAndMoreDto(this Chapter chapter)
    {
        var dto = new ChapterAndMoreDto
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

    public static Chapter ConvertToChapter(this CreateChapterDto createChapterDto)
    {
        return new Chapter
        {
            ChapterId = Guid.NewGuid().ToString(),
            Title = createChapterDto.Title,
            ChapterNumber = createChapterDto.ChapterNumber,
            VolumeNumber = createChapterDto.VolumeNumber,
            MangaId = createChapterDto.MangaId.ToString()!
        };
    }
}