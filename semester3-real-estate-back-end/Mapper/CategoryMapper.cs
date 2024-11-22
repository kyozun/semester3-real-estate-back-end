using semester3_real_estate_back_end.DTO.Category;
using semester3_real_estate_back_end.Models;

namespace semester3_real_estate_back_end.Mapper;

public static class CategoryMapper
{
    public static CategoryDto ConvertToCategoryDto(this Category category)
    {
        return new CategoryDto
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
        };
    }


    public static CategoryAndMoreDto ConvertToCategoryAndMoreDto(this Category category)
    {
        var dto = new CategoryAndMoreDto
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            Properties = category.Properties.Select(x => x.ConvertToPropertyDto()).ToList(),
        };

        return dto;
    }

    
    // Convert to Entity
    public static Category ConvertToCategory(this CreateCategoryDto createCategoryDto)
    {
        var category = new Category
        {
            CategoryId = Guid.NewGuid().ToString(),
            Name = createCategoryDto.Name,
        };

        return category;
    }
}