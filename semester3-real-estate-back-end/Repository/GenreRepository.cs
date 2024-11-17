using System.Net;
using System.Runtime.InteropServices;
using Microsoft.IdentityModel.Tokens;
using semester4.DTO.Genre;
using semester4.Helpers.Enums.Include;
using semester4.Helpers.Query;
using semester4.Interfaces;

namespace semester4.Repository;

public class GenreRepository : IGenreRepository
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GenreRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<Genre>> GetGenres(GenreQuery genreQuery)
    {
        var genres = _context.Genre.AsQueryable();

        // Tim kiem theo GenreId
        if (!genreQuery.GenreId.IsNullOrEmpty())
            genres = genres.Where(x =>
                genreQuery.GenreId!.Select(genreId => genreId.ToString()).Contains(x.GenreId));

        // Tim kiem theo Name
        if (!string.IsNullOrWhiteSpace(genreQuery.Name))
            genres = genres.Where(x => x.Name.ToLower().Contains(genreQuery.Name.ToLower()));

        // Lấy thêm Include
        if (!genreQuery.Includes.IsNullOrEmpty())
            genres = genreQuery.Includes!.Aggregate(genres, (current, include) => include switch
            {
                GenreInclude.Manga => current.Include(x => x.MangaGenres).ThenInclude(x => x.Manga),
                _ => current
            });

        return await genres.Skip(genreQuery.Offset).Take(genreQuery.Limit).ToListAsync();
    }

    public async Task<Genre?> GetGenreById(string genreId, [Optional] List<GenreInclude> includes)
    {
        var genres = _context.Genre.AsQueryable();

        // Lấy thêm Include
        if (!includes.IsNullOrEmpty())
            genres = includes.Aggregate(genres, (current, include) => include switch
            {
                GenreInclude.Manga => current.Include(x => x.MangaGenres).ThenInclude(x => x.Manga),
                _ => current
            });

        var genre = await genres.FirstOrDefaultAsync(x => x.GenreId == genreId);
        return genre;
    }

    public async Task<HttpStatusCode> CreateGenre(Genre genre)
    {
        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Kiểm tra user đã tạo Genre chưa
            var existingGenre = await _context.Genre.FirstOrDefaultAsync(x => x.Name.ToLower() == genre.Name.ToLower());

            if (existingGenre != null)
                // Nếu đã tồn tại, trả về lỗi Conflict (409)
                return HttpStatusCode.Conflict;

            // Lưu vào database
            await _context.AddAsync(genre);
            await _context.SaveChangesAsync();

            // Commit transaction nếu các dòng ở trên thành công
            await transaction.CommitAsync();
        }

        catch (Exception)
        {
            // Rollback transaction nếu có lỗi
            await transaction.RollbackAsync();
            return HttpStatusCode.BadRequest;
        }

        return HttpStatusCode.OK;
    }

    public async Task<HttpStatusCode> UpdateGenre(UpdateGenreDto updateGenreDto)
    {
        var isAdmin = _httpContextAccessor.HttpContext!.User.IsInRole("Admin");

        // Tìm genre theo genreId, nếu ko có trả về NotFound
        var genre = await GetGenreById(updateGenreDto.GenreId.ToString()!);

        if (genre == null) return HttpStatusCode.NotFound;

        // Kiểm tra name da ton tai chua
        if (updateGenreDto.Name != null)
        {
            var existingGenre =
                await _context.Genre.FirstOrDefaultAsync(x => x.Name.ToLower() == updateGenreDto.Name.ToLower());
            if (existingGenre != null)
                // Nếu đã tồn tại, trả về lỗi Conflict (409)
                return HttpStatusCode.Conflict;
        }

        // Nếu user không phải là admin, đồng thời ko là chủ sở hữu thì báo lỗi 403
        if (!isAdmin) return HttpStatusCode.Forbidden;

        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();


        // Cap nhat lai genre
        genre.Name = updateGenreDto.Name;
        genre.UpdatedAt = DateTime.Now;

        try
        {
            await _context.SaveChangesAsync();
            // Commit transaction nếu các dòng ở trên thành công
            await transaction.CommitAsync();
        }

        catch (Exception)
        {
            // Rollback transaction nếu có lỗi
            await transaction.RollbackAsync();
            return HttpStatusCode.BadRequest;
        }

        return HttpStatusCode.OK;
    }

    public async Task<HttpStatusCode> DeleteGenre(List<Guid> genreIds)
    {
        var isAdmin = _httpContextAccessor.HttpContext!.User.IsInRole("Admin");

        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Tìm tất cả genre theo Id
            var genres = await _context.Genre
                .Where(x => genreIds.Select(genreId => genreId.ToString()).Contains(x.GenreId))
                .ToListAsync();

            // Nếu không tìm thấy
            if (genres.IsNullOrEmpty()) return HttpStatusCode.NotFound;

            // Nếu user không phải là admin thì báo lỗi 403
            if (!isAdmin) return HttpStatusCode.Forbidden;

            _context.Genre.RemoveRange(genres);
            await _context.SaveChangesAsync();
            // Commit transaction nếu các dòng ở trên thành công
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            // Rollback transaction nếu có lỗi
            await transaction.RollbackAsync();
            return HttpStatusCode.BadRequest;
        }

        return HttpStatusCode.OK;
    }

    public async Task<bool> GenreExistsAsync(string genreId)
    {
        return await _context.Genre.AnyAsync(x => x.GenreId == genreId);
    }
}