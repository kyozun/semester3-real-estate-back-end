using System.Net;
using System.Runtime.InteropServices;
using semester3_real_estate_back_end.Models;

namespace semester3_real_estate_back_end.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategories(CategoryQuery categoryQuery);
    Task<Category?> GetCategoryById(string categoryId, [Optional] List<CategoryInclude> includes);
    Task<HttpStatusCode> CreateCategory(Category category);
    Task<HttpStatusCode> UpdateCategory(UpdateCategoryDto updateCategoryDto);
    Task<HttpStatusCode> DeleteCategory(List<Guid> categoryIds);
    Task<bool> CategoryExistsAsync(string categoryId);
}