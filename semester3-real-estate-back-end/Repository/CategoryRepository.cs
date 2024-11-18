using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using semester3_real_estate_back_end.Data;
using semester3_real_estate_back_end.DTO.Category;
using semester3_real_estate_back_end.Helpers.Query;
using semester3_real_estate_back_end.Interfaces;
using semester3_real_estate_back_end.Models;

namespace semester3_real_estate_back_end.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CategoryRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<Category>> GetCategories(CategoryQuery categoryQuery)
    {
        var categories = _context.Category.AsQueryable();

        // Tìm theo Name
        if (!string.IsNullOrWhiteSpace(categoryQuery.Name))
            categories = categories.Where(x => x.Name.ToLower().Contains(categoryQuery.Name.ToLower()));

        // Lấy thêm Include
        categories = categories.Include(x => x.Properties).ThenInclude(x => x.PropertyImages);

        return await categories.Skip(categoryQuery.Offset).Take(categoryQuery.Limit).ToListAsync();
    }

    public async Task<Category?> GetCategoryById(string categoryId)
    {
        var categories = _context.Category.AsQueryable();

        // Lấy thêm Include
        categories = categories.Include(x => x.Properties).ThenInclude(x => x.PropertyImages);

        var category = await categories.FirstOrDefaultAsync(x => x.CategoryId == categoryId);
        return category;
    }

    public async Task<HttpStatusCode> CreateCategory(Category category)
    {
        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Kiểm tra name đã tồn tại chưa
            var existingCategory =
                await _context.Category.FirstOrDefaultAsync(x => x.Name.ToLower() == category.Name.ToLower());

            if (existingCategory != null)
                // Nếu đã tồn tại, trả về lỗi Conflict (409)
                return HttpStatusCode.Conflict;

            // Lưu vào database
            await _context.AddAsync(category);
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


    public async Task<HttpStatusCode> UpdateCategory(UpdateCategoryDto updateCategoryDto)
    {
        var isAdmin = _httpContextAccessor.HttpContext!.User.IsInRole("Admin");

        // Tìm category theo categoryId, nếu ko có trả về NotFound
        var category = await GetCategoryById(updateCategoryDto.CategoryId.ToString()!);

        if (category == null) return HttpStatusCode.NotFound;

        if (updateCategoryDto.Name != null)
        {
            var existingCategory =
                await _context.Category.FirstOrDefaultAsync(x =>
                    x.Name.ToLower() == updateCategoryDto.Name.ToLower());
            if (existingCategory != null)
                // Nếu đã tồn tại, trả về lỗi Conflict (409)
                return HttpStatusCode.Conflict;
        }

        // Nếu user không phải là admin thì báo lỗi 403
        if (!isAdmin) return HttpStatusCode.Forbidden;

        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();


        // Cập nhật lại category
        if (updateCategoryDto.Name != null) category.Name = updateCategoryDto.Name;
        category.UpdatedAt = DateTime.Now;
      

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

    public async Task<HttpStatusCode> DeleteCategory(List<Guid> categoryIds)
    {
        var isAdmin = _httpContextAccessor.HttpContext!.User.IsInRole("Admin");

        // Bắt đầu transaction
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Tìm tất cả category theo Id
            var categories = await _context.Category
                .Where(x => categoryIds.Select(categoryId => categoryId.ToString()).Contains(x.CategoryId))
                .ToListAsync();

            // Nếu không tìm thấy
            if (categories.IsNullOrEmpty()) return HttpStatusCode.NotFound;

            // Nếu user không phải là admin thì báo lỗi 403
            if (!isAdmin) return HttpStatusCode.Forbidden;

            _context.Category.RemoveRange(categories);
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

    public async Task<bool> CategoryExistsAsync(string categoryId)
    {
        return await _context.Category.AnyAsync(x => x.CategoryId == categoryId);
    }
}