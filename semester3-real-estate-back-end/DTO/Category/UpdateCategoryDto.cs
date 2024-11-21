using Microsoft.Build.Framework;

namespace semester3_real_estate_back_end.DTO.Category;

public class UpdateCategoryDto
{
    [Required] public Guid? CategoryId { get; set; }
    public string Name { get; set; }
}