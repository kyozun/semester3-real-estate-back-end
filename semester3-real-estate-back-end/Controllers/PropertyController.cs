using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using semester3_real_estate_back_end.DTO.Property;
using semester3_real_estate_back_end.Helpers.Query;
using semester3_real_estate_back_end.Interfaces;
using semester3_real_estate_back_end.Mapper;
using semester3_real_estate_back_end.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace semester3_real_estate_back_end.Controllers;

[ApiController]
[Route("api/property")]
public class PropertyController : ControllerBase
{
    private readonly IPropertyRepository _propertyRepository;

    public PropertyController(IPropertyRepository propertyRepository
    )
    {
        _propertyRepository = propertyRepository;
    }

    [SwaggerOperation(Summary = "Tìm Property theo điều kiện")]
    [HttpGet]
    public async Task<ActionResult<Response<PropertyAndMoreDto>>> GetProperties(
        [FromQuery] PropertyQuery propertyQuery)
    {
        var properties = await _propertyRepository.GetProperties(propertyQuery);
        var propertyAndMoresDto = properties.Select(x => x.ConvertToPropertyAndMoreDto()).ToList();
        var total = propertyAndMoresDto.Count;
        var limit = propertyQuery.Limit;
        var offset = propertyQuery.Offset;
        return Ok(new Response<PropertyAndMoreDto>(propertyAndMoresDto, total, limit, offset));
    }


    // GET BY ID
    [SwaggerOperation(Summary = "Tìm Property theo ID")]
    [HttpGet]
    [Route("{propertyId:guid}")]
    public async Task<ActionResult<PropertyAndMoreDto>> GetPropertyById([FromRoute] Guid propertyId)
    {
        try
        {
            var property = await _propertyRepository.GetPropertyById(propertyId.ToString());
            if (property == null) return NotFound();
            return Ok(property.ConvertToPropertyAndMoreDto());
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }


    // POST
    [SwaggerOperation(Summary = "Tạo Property")]
    [HttpPost]
    [Authorize]
    public async Task<ActionResult> CreateProperty([FromForm] CreatePropertyDto createPropertyDto
        )
    {
        try
        {
            // var property = createPropertyDto.ConvertToProperty();
            var result = await _propertyRepository.CreateProperty(createPropertyDto);
            return result switch
            {
                HttpStatusCode.OK => Ok(new OkResponse()),
                HttpStatusCode.Forbidden => Forbid(),
                HttpStatusCode.Conflict => Conflict(),
                _ => BadRequest()
            };
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }


    // PUT
    [SwaggerOperation(Summary = "Cập nhật Property")]
    [HttpPut]
    // [Authorize]
    public async Task<ActionResult> UpdateProperty([FromForm] UpdatePropertyDto updatePropertyDto)
    {
        try
        {
            var result = await _propertyRepository.UpdateProperty(updatePropertyDto);
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
            if (!await _propertyRepository.PropertyExistsAsync(updatePropertyDto.PropertyId.ToString()!))
                return NotFound();

            return BadRequest();
        }
    }

    // DELETE
    [SwaggerOperation(Summary = "Xóa Property")]
    [HttpDelete]
    [Authorize]
    public async Task<ActionResult> DeleteProperty([FromBody] List<Guid> propertyIds)
    {
        try
        {
            var result = await _propertyRepository.DeleteProperty(propertyIds);
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