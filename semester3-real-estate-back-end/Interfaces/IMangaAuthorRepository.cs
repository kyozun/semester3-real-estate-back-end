namespace semester4.Interfaces;

public interface IMangaAuthorRepository
{
    Task<MangaAuthor> CreateMangaAuthor(MangaAuthor mangaAuthor);
}