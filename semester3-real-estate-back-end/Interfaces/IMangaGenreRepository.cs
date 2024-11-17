namespace semester4.Interfaces;

public interface IMangaGenreRepository
{
    Task<MangaGenre> CreateMangaGenre(MangaGenre mangaGenre);
}