using System.Net;
using System.Runtime.InteropServices;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using semester4.DTO.Chapter;
using semester4.Helpers.Enums;
using semester4.Helpers.Enums.Include;
using semester4.Helpers.Query;
using semester4.Interfaces;

namespace semester4.Repository;

public class ChapterRepository : IChapterRepository
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ChapterRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<Chapter>> GetChapters(ChapterQuery chapterQuery)
    {
        var chapters = _context.Chapter.AsQueryable();

        // Tìm theo ChapterId
        if (!chapterQuery.ChapterId.IsNullOrEmpty())
            chapters = chapters.Where(x =>
                chapterQuery.ChapterId!.Select(chapterId => chapterId.ToString()).Contains(x.ChapterId));

        // Tìm theo Title
        if (!string.IsNullOrWhiteSpace(chapterQuery.Title))
            chapters = chapters.Where(x => x.Title.ToLower().Contains(chapterQuery.Title.ToLower()));

        // Tìm theo MangaId
        if (!chapterQuery.MangaId.IsNullOrEmpty())
            chapters = chapters.Where(x =>
                chapterQuery.MangaId!.Select(mangaId => mangaId.ToString()).ToList().Contains(x.MangaId));

        // Tìm theo UserId
        if (!chapterQuery.UserId.IsNullOrEmpty())
            chapters = chapters.Where(x =>
                chapterQuery.UserId!.Select(userId => userId.ToString()).Contains(x.UserId));


        // Tìm theo VolumeNumber
        if (!chapterQuery.VolumeNumber.IsNullOrEmpty())
            chapters = chapters.Where(x => chapterQuery.VolumeNumber!.Contains(x.VolumeNumber));

        // Tìm theo ChapterNumber
        if (!chapterQuery.ChapterNumber.IsNullOrEmpty())
            chapters = chapters.Where(x => chapterQuery.ChapterNumber!.Contains(x.ChapterNumber));

        // // Tìm theo ContentRating
        // if (!chapterQuery.ContentRating.IsNullOrEmpty())
        // {
        //     chapters = chapters.Include(x => x.Manga)
        //         .Where(x => x.Manga != null &&
        //                     x.Manga.ContentRating.Any(contentRating => chapterQuery.ContentRating!.Contains(contentRating)));
        // }

        // Lấy thêm Include
        if (!chapterQuery.Includes.IsNullOrEmpty())
            chapters = chapterQuery.Includes!.Aggregate(chapters, (current, include) => include switch
            {
                ChapterInclude.Manga => current.Include(x => x.Manga),
                ChapterInclude.User => current.Include(x => x.User),
                _ => current
            });


        // Sắp xếp theo CreatedAt
        if (!string.IsNullOrWhiteSpace(chapterQuery.OrderBy?.CreatedAt.ToString()))
            chapters = chapterQuery.OrderBy.CreatedAt == OrderBy.Asc
                ? chapters.OrderBy(x => x.CreatedAt)
                : chapters.OrderByDescending(x => x.CreatedAt);

        // Sắp xếp theo UpdatedAt
        if (!string.IsNullOrWhiteSpace(chapterQuery.OrderBy?.UpdatedAt.ToString()))
            chapters = chapterQuery.OrderBy.UpdatedAt == OrderBy.Asc
                ? chapters.OrderBy(x => x.UpdatedAt)
                : chapters.OrderByDescending(x => x.UpdatedAt);

        // Sắp xếp theo Volume Number
        if (!string.IsNullOrWhiteSpace(chapterQuery.OrderBy?.VolumeNumber.ToString()))
            chapters = chapterQuery.OrderBy.VolumeNumber == OrderBy.Asc
                ? chapters.OrderBy(x => x.VolumeNumber)
                : chapters.OrderByDescending(x => x.VolumeNumber);


        // Sắp xếp theo Chapter Number
        if (!string.IsNullOrWhiteSpace(chapterQuery.OrderBy?.ChapterNumber.ToString()))
            chapters = chapterQuery.OrderBy.ChapterNumber == OrderBy.Asc
                ? chapters.OrderBy(x => x.ChapterNumber)
                : chapters.OrderByDescending(x => x.ChapterNumber);


        return await chapters.Skip(chapterQuery.Offset).Take(chapterQuery.Limit).ToListAsync();
    }

    public async Task<Chapter?> GetChapterById(string chapterId, [Optional] List<ChapterInclude> includes)
    {
        var chapters = _context.Chapter.AsQueryable();

        // Lấy thêm Include
        if (!includes.IsNullOrEmpty())
            chapters = includes.Aggregate(chapters, (current, include) => include switch
            {
                ChapterInclude.Manga => current.Include(x => x.Manga),
                ChapterInclude.User => current.Include(x => x.User),
                _ => current
            });

        var chapter = await chapters.FirstOrDefaultAsync(x => x.ChapterId == chapterId);
        return chapter;
    }

    public async Task<HttpStatusCode> CreateChapter(Chapter chapter)
    {
        var userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        chapter.UserId = userId;

        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Kiểm tra title đã tồn tại chưa
            var existingChapter =
                await _context.Chapter.FirstOrDefaultAsync(x => x.Title.ToLower() == chapter.Title.ToLower());

            if (existingChapter != null)
                // Nếu đã tồn tại, trả về lỗi Conflict (409)
                return HttpStatusCode.Conflict;

            // Lưu vào database
            await _context.AddAsync(chapter);
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

    public async Task<HttpStatusCode> UpdateChapter(UpdatePropertyImageDto updatePropertyImageDto)
    {
        var isAdmin = _httpContextAccessor.HttpContext!.User.IsInRole("Admin");

        // Tìm chapter theo chapterId, nếu ko có trả về NotFound
        var chapter = await GetChapterById(updatePropertyImageDto.ChapterId.ToString()!);

        if (chapter == null) return HttpStatusCode.NotFound;

        if (updatePropertyImageDto.Title != null)
        {
            var existingChapter =
                await _context.Chapter.FirstOrDefaultAsync(x => x.Title.ToLower() == updatePropertyImageDto.Title.ToLower());
            if (existingChapter != null)
                // Nếu đã tồn tại, trả về lỗi Conflict (409)
                return HttpStatusCode.Conflict;
        }

        // Nếu user không phải là admin, đồng thời ko là chủ sở hữu thì báo lỗi 403
        if (!isAdmin) return HttpStatusCode.Forbidden;

        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();


        // Cập nhật lại chapter
        if (updatePropertyImageDto.Title != null) chapter.Title = updatePropertyImageDto.Title;
        if (updatePropertyImageDto.ChapterNumber.HasValue) chapter.ChapterNumber = (int)updatePropertyImageDto.ChapterNumber;
        if (updatePropertyImageDto.VolumeNumber.HasValue) chapter.VolumeNumber = (int)updatePropertyImageDto.VolumeNumber;
        chapter.UpdatedAt = DateTime.Now;

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

    public async Task<HttpStatusCode> DeleteChapter(List<Guid> chapterIds)
    {
        var isAdmin = _httpContextAccessor.HttpContext!.User.IsInRole("Admin");

        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Tìm tất cả chapter theo Id
            var chapters = await _context.Chapter
                .Where(x => chapterIds.Select(chapterId => chapterId.ToString()).Contains(x.ChapterId))
                .ToListAsync();

            // Nếu không tìm thấy
            if (chapters.IsNullOrEmpty()) return HttpStatusCode.NotFound;

            // Nếu user không phải là admin thì báo lỗi 403
            if (!isAdmin) return HttpStatusCode.Forbidden;

            _context.Chapter.RemoveRange(chapters);
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

    public async Task<bool> ChapterExistsAsync(string chapterId)
    {
        return await _context.Chapter.AnyAsync(x => x.ChapterId == chapterId);
    }
}