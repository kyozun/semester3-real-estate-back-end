using System.Net;
using Microsoft.AspNetCore.Mvc;
using semester3_real_estate_back_end.DTO.Juridical;
using semester3_real_estate_back_end.Helpers.Query;
using semester3_real_estate_back_end.Interfaces;
using semester3_real_estate_back_end.Mapper;
using semester3_real_estate_back_end.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace semester3_real_estate_back_end.Controllers;

[ApiController]
[Route("api/juridical")]
public class JuridicalController : ControllerBase
{
    private readonly IJuridicalRepository _juridicalsRepository;

    public JuridicalController(IJuridicalRepository juridicalsRepository)
    {
        _juridicalsRepository = juridicalsRepository;
    }

    // GET
    [SwaggerOperation(Summary = "Tìm Juridical theo điều kiện")]
    [HttpGet]
    public async Task<ActionResult<Response<JuridicalAndMoreDto>>> GetJuridicals(
        [FromQuery] JuridicalQuery juridicalsQuery)
    {
        var juridicals = await _juridicalsRepository.GetJuridicals(juridicalsQuery);
        var juridicalsAndMoreDtos = juridicals.Select(x => x.ConvertToJuridicalAndMoreDto()).ToList();
        var total = juridicalsAndMoreDtos.Count;
        var limit = juridicalsQuery.Limit;
        var offset = juridicalsQuery.Offset;
        return Ok(new Response<JuridicalAndMoreDto>(juridicalsAndMoreDtos, total, limit, offset));
    }

    // GET BY ID
    [HttpGet]
    [SwaggerOperation(Summary = "Lấy Juridical theo ID")]
    [Route("{juridicalsId:guid}")]
    public async Task<ActionResult<JuridicalAndMoreDto>> GetJuridical([FromRoute] Guid juridicalsId)
    {
        try
        {
            var juridicals = await _juridicalsRepository.GetJuridicalById(juridicalsId.ToString());
            if (juridicals == null) return NotFound();
            return Ok(juridicals.ConvertToJuridicalAndMoreDto());
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    // POST
    [SwaggerOperation(Summary = "Tạo Juridical")]
    [HttpPost]
    public async Task<ActionResult> CreateJuridical([FromBody] CreateJuridicalDto createJuridicalDto)
    {
        try
        {
            var juridicals = createJuridicalDto.ConvertToJuridical();
            var result = await _juridicalsRepository.CreateJuridical(juridicals);
            return result switch
            {
                HttpStatusCode.OK => Ok(new OkResponse()),
                HttpStatusCode.Forbidden => Forbid(),
                HttpStatusCode.Conflict => Conflict(new ConflictResponse(
                    $"An existing record with the name '{createJuridicalDto.Name}' was already found"
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
    [SwaggerOperation(Summary = "Cập nhật Juridical")]
    [HttpPut]
    public async Task<ActionResult> UpdateJuridical([FromBody] UpdateJuridicalDto updateJuridicalDto)
    {
        try
        {
            var result = await _juridicalsRepository.UpdateJuridical(updateJuridicalDto);
            return result switch
            {
                HttpStatusCode.OK => Ok(new OkResponse()),
                HttpStatusCode.Forbidden => Forbid(),
                HttpStatusCode.NotFound => NotFound(),
                HttpStatusCode.Conflict => Conflict(new ConflictResponse(
                    $"An existing record with the name '{updateJuridicalDto.Name}' was already found"
                )),
                _ => BadRequest()
            };
        }
        catch (Exception)
        {
            if (!await _juridicalsRepository.JuridicalExistsAsync(updateJuridicalDto.JuridicalId.ToString()!))
                return NotFound();

            return BadRequest();
        }
    }


    // DELETE
    [SwaggerOperation(Summary = "Xóa Juridical")]
    [HttpDelete]
    public async Task<ActionResult> DeleteJuridical([FromBody] List<Guid> juridicalsIds)
    {
        try
        {
            var result = await _juridicalsRepository.DeleteJuridical(juridicalsIds);
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