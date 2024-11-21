using System.Net;
using Microsoft.AspNetCore.Mvc;
using semester3_real_estate_back_end.DTO.Ward;
using semester3_real_estate_back_end.Helpers.Query;
using semester3_real_estate_back_end.Interfaces;
using semester3_real_estate_back_end.Mapper;
using semester3_real_estate_back_end.Wrapper;
using semester4.DTO.Genre;
using Swashbuckle.AspNetCore.Annotations;

namespace semester3_real_estate_back_end.Controllers;

[ApiController]
[Route("api/ward")]
public class WardController : ControllerBase
{
    private readonly IWardRepository _wardRepository;

    public WardController(IWardRepository wardRepository)
    {
        _wardRepository = wardRepository;
    }

    // GET
    [SwaggerOperation(Summary = "Tìm Ward theo điều kiện")]
    [HttpGet]
    public async Task<ActionResult<Response<WardAndMoreDto>>> GetWards(
        [FromQuery] WardQuery wardQuery)
    {
        var ward = await _wardRepository.GetWards(wardQuery);
        var wardAndMoreDtos = ward.Select(x => x.ConvertToWardAndMoreDto()).ToList();
        var total = wardAndMoreDtos.Count;
        var limit = wardQuery.Limit;
        var offset = wardQuery.Offset;
        return Ok(new Response<WardAndMoreDto>(wardAndMoreDtos, total, limit, offset));
    }

    // GET BY ID
    [HttpGet]
    [SwaggerOperation(Summary = "Lấy Ward theo ID")]
    [Route("{wardId:guid}")]
    public async Task<ActionResult<WardAndMoreDto>> GetWard([FromRoute] int wardId)
    {
        try
        {
            var ward = await _wardRepository.GetWardById(wardId);
            if (ward == null) return NotFound();
            return Ok(ward.ConvertToWardAndMoreDto());
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    // POST
    [SwaggerOperation(Summary = "Tạo Ward")]
    [HttpPost]
    public async Task<ActionResult> CreateWard([FromBody] CreateWardDto createWardDto)
    {
        try
        {
            var ward = createWardDto.ConvertToWard();
            var result = await _wardRepository.CreateWard(ward);
            return result switch
            {
                HttpStatusCode.OK => Ok(new OkResponse()),
                HttpStatusCode.Forbidden => Forbid(),
                HttpStatusCode.Conflict => Conflict(new ConflictResponse(
                    $"An existing record with the name '{createWardDto.Name}' was already found"
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
    [SwaggerOperation(Summary = "Cập nhật Ward")]
    [HttpPut]
    public async Task<ActionResult> UpdateWard([FromBody] UpdateWardDto updateWardDto)
    {
        try
        {
            var result = await _wardRepository.UpdateWard(updateWardDto);
            return result switch
            {
                HttpStatusCode.OK => Ok(new OkResponse()),
                HttpStatusCode.Forbidden => Forbid(),
                HttpStatusCode.NotFound => NotFound(),
                HttpStatusCode.Conflict => Conflict(new ConflictResponse(
                    $"An existing record with the name '{updateWardDto.Name}' was already found"
                )),
                _ => BadRequest()
            };
        }
        catch (Exception)
        {
            if (!await _wardRepository.WardExistsAsync(updateWardDto.WardId))
                return NotFound();

            return BadRequest();
        }
    }


    // DELETE
    [SwaggerOperation(Summary = "Xóa Ward")]
    [HttpDelete]
    public async Task<ActionResult> DeleteWard([FromBody] List<int> wardIds)
    {
        try
        {
            var result = await _wardRepository.DeleteWard(wardIds);
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