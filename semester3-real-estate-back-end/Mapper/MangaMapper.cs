using semester4.DTO.Manga;

namespace semester4.Mapper;

public static class MangaMapper
{
    public static MangaDto ConvertToMangaDto(this Manga manga)
    {
        return new MangaDto
        {
            Title = manga.Title,
            Description = manga.Description,
            ViewCount = manga.ViewCount,
            MangaStatus = manga.MangaStatus
        };
    }

    public static MangaAndMoreDto ConvertToMangaAndMoreDto(this Manga manga)
    {
        var dto = new MangaAndMoreDto
        {
            MangaId = manga.MangaId,
            Title = manga.Title,
            Description = manga.Description,
            Year = manga.Year,
            Rating = manga.Ratings.Select(x => x.Score).DefaultIfEmpty(0).Average(),
            ViewCount = manga.ViewCount,
            MangaStatus = manga.MangaStatus.ToString(),
            LastVolume = manga.Chapters.Select(x => x.ChapterNumber).OrderByDescending(x => x).FirstOrDefault(0),
            LastChapter = manga.Chapters.Select(x => x.VolumeNumber).OrderByDescending(x => x).FirstOrDefault(0),
            IsLock = manga.IsLock
        };


        if (!manga.MangaGenres.IsNullOrEmpty())
            dto.Genres = manga.MangaGenres.Select(x => x.Genre!.ConvertToGenreDto()).ToList();

        if (!manga.MangaAuthors.IsNullOrEmpty())
            dto.Authors = manga.MangaAuthors.Select(x => x.Author!.ConvertToAuthorDto()).ToList();

        if (!manga.MangaContentRatings.IsNullOrEmpty())
            dto.ContentRatings = manga.MangaContentRatings.Select(x => x.ContentRating!.ConvertToContentRatingDto())
                .ToList();

        if (!manga.CoverArts.IsNullOrEmpty())
            dto.CoverArts = manga.CoverArts.Select(x => x.ConvertToCoverArtDto()!).ToList();

        return dto;
    }

    public static Manga ConvertToManga(this CreateMangaDto createMangaDto)
    {
        return new Manga
        {
            MangaId = Guid.NewGuid().ToString(),
            Title = createMangaDto.Title,
            Description = createMangaDto.Description,
            MangaStatus = createMangaDto.MangaStatus,
            Year = createMangaDto.Year,
            MangaGenres = createMangaDto.GenreIds.Select(x => new MangaGenre { GenreId = x.ToString() }).ToList(),
            MangaAuthors = createMangaDto.AuthorIds.Select(x => new MangaAuthor { AuthorId = x.ToString() }).ToList(),
            MangaContentRatings = createMangaDto.ContentRatingIds.Select(x => new MangaContentRating
                { MangaContentRatingId = Guid.NewGuid().ToString(), ContentRatingId = x.ToString() }).ToList()
        };
    }
}