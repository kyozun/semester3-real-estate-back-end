using System.Net;
using Microsoft.AspNetCore.Mvc;
using semester3_real_estate_back_end.DTO.Category;
using semester3_real_estate_back_end.Helpers.Query;
using semester3_real_estate_back_end.Interfaces;
using semester3_real_estate_back_end.Mapper;
using semester3_real_estate_back_end.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace semester3_real_estate_back_end.Controllers;

[ApiController]
[Route("api/category")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _categoriesRepository;

    public CategoryController(ICategoryRepository categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    // GET
    [SwaggerOperation(Summary = "Tìm Category theo điều kiện")]
    [HttpGet]
    public async Task<ActionResult<Response<CategoryAndMoreDto>>> GetCategorys(
        [FromQuery] CategoryQuery categoriesQuery)
    {
        var categories = await _categoriesRepository.GetCategories(categoriesQuery);
        var categoriesAndMoreDtos = categories.Select(x => x.ConvertToCategoryAndMoreDto()).ToList();
        var total = categoriesAndMoreDtos.Count;
        var limit = categoriesQuery.Limit;
        var offset = categoriesQuery.Offset;
        return Ok(new Response<CategoryAndMoreDto>(categoriesAndMoreDtos, total, limit, offset));
    }

    // GET BY ID
    [HttpGet]
    [SwaggerOperation(Summary = "Lấy Category theo ID")]
    [Route("{categoriesId:guid}")]
    public async Task<ActionResult<CategoryAndMoreDto>> GetCategory([FromRoute] Guid categoriesId)
    {
        try
        {
            var categories = await _categoriesRepository.GetCategoryById(categoriesId.ToString());
            if (categories == null) return NotFound();
            return Ok(categories.ConvertToCategoryAndMoreDto());
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    // POST
    [SwaggerOperation(Summary = "Tạo Category")]
    [HttpPost]
    public async Task<ActionResult> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
    {
        try
        {
            var categories = createCategoryDto.ConvertToCategory();
            var result = await _categoriesRepository.CreateCategory(categories);
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
    public async Task<ActionResult> UpdateCategory([FromBody] UpdateCategoryDto updateCategoryDto)
    {
        try
        {
            var result = await _categoriesRepository.UpdateCategory(updateCategoryDto);
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
            if (!await _categoriesRepository.CategoryExistsAsync(updateCategoryDto.CategoryId.ToString()!))
                return NotFound();

            return BadRequest();
        }
    }


    // DELETE
    [SwaggerOperation(Summary = "Xóa Category")]
    [HttpDelete]
    public async Task<ActionResult> DeleteCategory([FromBody] List<Guid> categoriesIds)
    {
        try
        {
            var result = await _categoriesRepository.DeleteCategory(categoriesIds);
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