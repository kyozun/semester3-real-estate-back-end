using semester4.Interfaces;

namespace semester4.Repository;

public class MangaGenreRepository : IMangaGenreRepository
{
    private readonly DataContext _context;

    public MangaGenreRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<MangaGenre> CreateMangaGenre(MangaGenre mangaGenre)
    {
        await _context.MangaGenre.AddAsync(mangaGenre);
        await _context.SaveChangesAsync();
        return mangaGenre;
    }
}