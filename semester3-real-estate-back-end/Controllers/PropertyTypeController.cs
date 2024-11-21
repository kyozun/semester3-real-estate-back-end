using System.Net;
using Microsoft.AspNetCore.Mvc;
using semester3_real_estate_back_end.DTO.PropertyType;
using semester3_real_estate_back_end.Helpers.Query;
using semester3_real_estate_back_end.Interfaces;
using semester3_real_estate_back_end.Mapper;
using semester3_real_estate_back_end.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace semester3_real_estate_back_end.Controllers;

[ApiController]
[Route("api/property-type")]
public class PropertyTypeController : ControllerBase
{
    private readonly IPropertyTypeRepository _propertyTypeRepository;

    public PropertyTypeController(IPropertyTypeRepository propertyTypeRepository)
    {
        _propertyTypeRepository = propertyTypeRepository;
    }

    // GET
    [SwaggerOperation(Summary = "Tìm PropertyType theo điều kiện")]
    [HttpGet]
    public async Task<ActionResult<Response<PropertyTypeAndMoreDto>>> GetPropertyTypes(
        [FromQuery] PropertyTypeQuery propertyTypeQuery)
    {
        var propertyType = await _propertyTypeRepository.GetPropertyTypes(propertyTypeQuery);
        var propertyTypeAndMoreDtos = propertyType.Select(x => x.ConvertToPropertyTypeAndMoreDto()).ToList();
        var total = propertyTypeAndMoreDtos.Count;
        var limit = propertyTypeQuery.Limit;
        var offset = propertyTypeQuery.Offset;
        return Ok(new Response<PropertyTypeAndMoreDto>(propertyTypeAndMoreDtos, total, limit, offset));
    }

    // GET BY ID
    [HttpGet]
    [SwaggerOperation(Summary = "Lấy PropertyType theo ID")]
    [Route("{propertyTypeId:guid}")]
    public async Task<ActionResult<PropertyTypeAndMoreDto>> GetPropertyType([FromRoute] Guid propertyTypeId)
    {
        try
        {
            var propertyType = await _propertyTypeRepository.GetPropertyTypeById(propertyTypeId.ToString());
            if (propertyType == null) return NotFound();
            return Ok(propertyType.ConvertToPropertyTypeAndMoreDto());
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    // POST
    [SwaggerOperation(Summary = "Tạo PropertyType")]
    [HttpPost]
    public async Task<ActionResult> CreatePropertyType([FromBody] CreatePropertyTypeDto createPropertyTypeDto)
    {
        try
        {
            var propertyType = createPropertyTypeDto.ConvertToPropertyType();
            var result = await _propertyTypeRepository.CreatePropertyType(propertyType);
            return result switch
            {
                HttpStatusCode.OK => Ok(new OkResponse()),
                HttpStatusCode.Forbidden => Forbid(),
                HttpStatusCode.Conflict => Conflict(new ConflictResponse(
                    $"An existing record with the name '{createPropertyTypeDto.Name}' was already found"
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
    [SwaggerOperation(Summary = "Cập nhật PropertyType")]
    [HttpPut]
    public async Task<ActionResult> UpdatePropertyType([FromBody] UpdatePropertyTypeDto updatePropertyTypeDto)
    {
        try
        {
            var result = await _propertyTypeRepository.UpdatePropertyType(updatePropertyTypeDto);
            return result switch
            {
                HttpStatusCode.OK => Ok(new OkResponse()),
                HttpStatusCode.Forbidden => Forbid(),
                HttpStatusCode.NotFound => NotFound(),
                HttpStatusCode.Conflict => Conflict(new ConflictResponse(
                    $"An existing record with the name '{updatePropertyTypeDto.Name}' was already found"
                )),
                _ => BadRequest()
            };
        }
        catch (Exception)
        {
            if (!await _propertyTypeRepository.PropertyTypeExistsAsync(updatePropertyTypeDto.PropertyTypeId.ToString()!))
                return NotFound();

            return BadRequest();
        }
    }


    // DELETE
    [SwaggerOperation(Summary = "Xóa PropertyType")]
    [HttpDelete]
    public async Task<ActionResult> DeletePropertyType([FromBody] List<Guid> propertyTypeIds)
    {
        try
        {
            var result = await _propertyTypeRepository.DeletePropertyType(propertyTypeIds);
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