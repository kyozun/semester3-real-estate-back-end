using semester4.DTO.Author;

namespace semester4.Mapper;

public static class AuthorMapper
{
    public static AuthorDto ConvertToAuthorDto(this Author author)
    {
        return new AuthorDto
        {
            Name = author.Name,
            Biography = author.Biography,
            YoutubeUrl = author.YoutubeUrl,
            TiktokUrl = author.TiktokUrl,
            WebsiteUrl = author.WebsiteUrl
        };
    }


    public static AuthorAndMoreDto ConvertToAuthorAndMoreDto(this Author author)
    {
        var dto = new AuthorAndMoreDto
        {
            Id = author.AuthorId,
            Name = author.Name,
            Biography = author.Biography,
            YoutubeUrl = author.YoutubeUrl,
            TiktokUrl = author.TiktokUrl,
            WebsiteUrl = author.WebsiteUrl
        };

        if (!author.MangaAuthors.IsNullOrEmpty())
            dto.Mangas = author.MangaAuthors.Select(x => x.Manga?.ConvertToMangaDto()).ToList();

        return dto;
    }

    public static Author ConvertToAuthor(this CreateAuthorDto createAuthorDto)
    {
        var author = new Author
        {
            AuthorId = Guid.NewGuid().ToString(),
            Name = createAuthorDto.Name,
            Biography = createAuthorDto.Biography
        };

        if (createAuthorDto.YoutubeUrl != null) author.YoutubeUrl = createAuthorDto.YoutubeUrl;

        if (createAuthorDto.TiktokUrl != null) author.TiktokUrl = createAuthorDto.TiktokUrl;

        if (createAuthorDto.WebsiteUrl != null) author.WebsiteUrl = createAuthorDto.WebsiteUrl;


        return author;
    }
}