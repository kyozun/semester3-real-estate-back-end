using System.Net;
using Microsoft.AspNetCore.Mvc;
using semester3_real_estate_back_end.DTO.Direction;
using semester3_real_estate_back_end.Helpers.Query;
using semester3_real_estate_back_end.Interfaces;
using semester3_real_estate_back_end.Mapper;
using semester3_real_estate_back_end.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace semester3_real_estate_back_end.Controllers;

[ApiController]
[Route("api/direction")]
public class DirectionController : ControllerBase
{
    private readonly IDirectionRepository _directionRepository;

    public DirectionController(IDirectionRepository directionRepository)
    {
        _directionRepository = directionRepository;
    }

    // GET
    [SwaggerOperation(Summary = "Tìm Direction theo điều kiện")]
    [HttpGet]
    public async Task<ActionResult<Response<DirectionAndMoreDto>>> GetDirections(
        [FromQuery] DirectionQuery directionQuery)
    {
        var direction = await _directionRepository.GetDirections(directionQuery);
        var directionAndMoreDtos = direction.Select(x => x.ConvertToDirectionAndMoreDto()).ToList();
        var total = directionAndMoreDtos.Count;
        var limit = directionQuery.Limit;
        var offset = directionQuery.Offset;
        return Ok(new Response<DirectionAndMoreDto>(directionAndMoreDtos, total, limit, offset));
    }

    // GET BY ID
    [HttpGet]
    [SwaggerOperation(Summary = "Lấy Direction theo ID")]
    [Route("{directionId:guid}")]
    public async Task<ActionResult<DirectionAndMoreDto>> GetDirection([FromRoute] Guid directionId)
    {
        try
        {
            var direction = await _directionRepository.GetDirectionById(directionId.ToString());
            if (direction == null) return NotFound();
            return Ok(direction.ConvertToDirectionAndMoreDto());
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    // POST
    [SwaggerOperation(Summary = "Tạo Direction")]
    [HttpPost]
    public async Task<ActionResult> CreateDirection([FromBody] CreateDirectionDto createDirectionDto)
    {
        try
        {
            var direction = createDirectionDto.ConvertToDirection();
            var result = await _directionRepository.CreateDirection(direction);
            return result switch
            {
                HttpStatusCode.OK => Ok(new OkResponse()),
                HttpStatusCode.Forbidden => Forbid(),
                HttpStatusCode.Conflict => Conflict(new ConflictResponse(
                    $"An existing record with the name '{createDirectionDto.Name}' was already found"
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
    [SwaggerOperation(Summary = "Cập nhật Direction")]
    [HttpPut]
    public async Task<ActionResult> UpdateDirection([FromBody] UpdateDirectionDto updateDirectionDto)
    {
        try
        {
            var result = await _directionRepository.UpdateDirection(updateDirectionDto);
            return result switch
            {
                HttpStatusCode.OK => Ok(new OkResponse()),
                HttpStatusCode.Forbidden => Forbid(),
                HttpStatusCode.NotFound => NotFound(),
                HttpStatusCode.Conflict => Conflict(new ConflictResponse(
                    $"An existing record with the name '{updateDirectionDto.Name}' was already found"
                )),
                _ => BadRequest()
            };
        }
        catch (Exception)
        {
            if (!await _directionRepository.DirectionExistsAsync(updateDirectionDto.DirectionId.ToString()!))
                return NotFound();

            return BadRequest();
        }
    }


    // DELETE
    [SwaggerOperation(Summary = "Xóa Direction")]
    [HttpDelete]
    public async Task<ActionResult> DeleteDirection([FromBody] List<Guid> directionIds)
    {
        try
        {
            var result = await _directionRepository.DeleteDirection(directionIds);
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