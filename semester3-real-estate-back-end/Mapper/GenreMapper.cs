using semester4.DTO.Genre;

namespace semester4.Mapper;

public static class GenreMapper
{
    public static GenreDto ConvertToGenreDto(this Genre genre)
    {
        return new GenreDto
        {
            Name = genre!.Name
        };
    }

    public static GenreAndMoreDto ConvertToGenreAndMoreDto(this Genre genre)
    {
        var dto = new GenreAndMoreDto
        {
            Id = genre.GenreId,
            Name = genre.Name
        };

        if (!genre.MangaGenres.IsNullOrEmpty())
            dto.Mangas = genre.MangaGenres.Select(x => x.Manga!.ConvertToMangaDto()).ToList();

        return dto;
    }

    public static Genre ConvertToGenre(this CreateGenreDto createGenreDto)
    {
        return new Genre
        {
            GenreId = Guid.NewGuid().ToString(),
            Name = createGenreDto.Name
        };
    }
}