using System.Net;
using Microsoft.AspNetCore.Mvc;
using semester3_real_estate_back_end.DTO.Category;
using semester3_real_estate_back_end.Helpers.Query;
using semester3_real_estate_back_end.Interfaces;
using semester4.Helpers.Enums.Include;
using semester4.Helpers.Query;
using semester4.Interfaces;
using semester4.Mapper;
using semester4.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace semester3_real_estate_back_end.Controllers;

[ApiController]
[Route("api/category")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    // GET
    [SwaggerOperation(Summary = "Tìm Category theo điều kiện")]
    [HttpGet]
    public async Task<ActionResult<Response<CategoryAndMoreDto>>> GetCategorys([FromQuery] CategoryQuery categoryQuery)
    {
        try
        {
            var categorys = await _categoryRepository.GetCategorys(categoryQuery);
            var categoryAndMoreDtos = categorys.Select(x => x.ConvertToCategoryAndMoreDto()).ToList();
            var total = categoryAndMoreDtos.Count;
            var limit = categoryQuery.Limit;
            var offset = categoryQuery.Offset;
            return Ok(new Response<CategoryAndMoreDto>(categoryAndMoreDtos, total, limit, offset));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    // GET BY ID
    [HttpGet]
    [SwaggerOperation(Summary = "Lấy Category theo ID")]
    [Route("{categoryId:guid}")]
    public async Task<ActionResult<CategoryAndMoreDto>> GetCategory([FromRoute] Guid categoryId,
        [FromQuery] List<CategoryInclude> includes)
    {
        try
        {
            var category = await _categoryRepository.GetCategoryById(categoryId.ToString(), includes);
            if (category == null) return NotFound();
            return Ok(category.ConvertToCategoryAndMoreDto());
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    // POST
    [SwaggerOperation(Summary = "Tạo Category")]
    [HttpPost]
    [Categoryize]
    public async Task<ActionResult> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
    {
        try
        {
            var category = createCategoryDto.ConvertToCategory();
            var result = await _categoryRepository.CreateCategory(category);
            return result switch
            {
                HttpStatusCode.OK => Ok(new OkResponse()),
                HttpStatusCode.Forbidden => Forbid(),
                HttpStatusCode.Conflict => Conflict(new ConflictResponse(
                    $"An existing record with the name '{createCategoryDto.Name}' was already found"
                )),

                _ => BadRequest()
            };
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    // PUT
    [SwaggerOperation(Summary = "Cập nhật Category")]
    [HttpPut]
    [Categoryize]
    public async Task<ActionResult> UpdateCategory([FromBody] UpdateCategoryDto updateCategoryDto)
    {
        try
        {
            var result = await _categoryRepository.UpdateCategory(updateCategoryDto);
            return result switch
            {
                HttpStatusCode.OK => Ok(new OkResponse()),
                HttpStatusCode.Forbidden => Forbid(),
                HttpStatusCode.NotFound => NotFound(),
                HttpStatusCode.Conflict => Conflict(new ConflictResponse(
                    $"An existing record with the name '{updateCategoryDto.Name}' was already found"
                )),
                _ => BadRequest()
            };
        }
        catch (Exception)
        {
            if (!await _categoryRepository.CategoryExistsAsync(updateCategoryDto.CategoryId.ToString()!)) return NotFound();

            return BadRequest();
        }
    }


    // DELETE
    [SwaggerOperation(Summary = "Xóa Category")]
    [HttpDelete]
    [Categoryize]
    public async Task<ActionResult> DeleteCategory([FromBody] List<Guid> categoryIds)
    {
        try
        {
            var result = await _categoryRepository.DeleteCategory(categoryIds);
            return result switch
            {
                HttpStatusCode.OK => Ok(new OkResponse()),
                HttpStatusCode.Forbidden => Forbid(),
                HttpStatusCode.NotFound => NotFound(),
                _ => BadRequest()
            };
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}