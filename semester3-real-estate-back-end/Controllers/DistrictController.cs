using System.Net;
using Microsoft.AspNetCore.Mvc;
using semester3_real_estate_back_end.DTO.District;
using semester3_real_estate_back_end.Helpers.Query;
using semester3_real_estate_back_end.Interfaces;
using semester3_real_estate_back_end.Mapper;
using semester3_real_estate_back_end.Wrapper;
using semester4.DTO.Genre;
using Swashbuckle.AspNetCore.Annotations;

namespace semester3_real_estate_back_end.Controllers;

[ApiController]
[Route("api/district")]
public class DistrictController : ControllerBase
{
    private readonly IDistrictRepository _districtRepository;

    public DistrictController(IDistrictRepository districtRepository)
    {
        _districtRepository = districtRepository;
    }

    // GET
    [SwaggerOperation(Summary = "Tìm District theo điều kiện")]
    [HttpGet]
    public async Task<ActionResult<Response<DistrictAndMoreDto>>> GetDistricts(
        [FromQuery] DistrictQuery districtQuery)
    {
        var district = await _districtRepository.GetDistricts(districtQuery);
        var districtAndMoreDtos = district.Select(x => x.ConvertToDistrictAndMoreDto()).ToList();
        var total = districtAndMoreDtos.Count;
        var limit = districtQuery.Limit;
        var offset = districtQuery.Offset;
        return Ok(new Response<DistrictAndMoreDto>(districtAndMoreDtos, total, limit, offset));
    }

    // GET BY ID
    [HttpGet]
    [SwaggerOperation(Summary = "Lấy District theo ID")]
    [Route("{districtId:guid}")]
    public async Task<ActionResult<DistrictAndMoreDto>> GetDistrict([FromRoute] int districtId)
    {
        try
        {
            var district = await _districtRepository.GetDistrictById(districtId);
            if (district == null) return NotFound();
            return Ok(district.ConvertToDistrictAndMoreDto());
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    // POST
    [SwaggerOperation(Summary = "Tạo District")]
    [HttpPost]
    public async Task<ActionResult> CreateDistrict([FromBody] CreateDistrictDto createDistrictDto)
    {
        try
        {
            var district = createDistrictDto.ConvertToDistrict();
            var result = await _districtRepository.CreateDistrict(district);
            return result switch
            {
                HttpStatusCode.OK => Ok(new OkResponse()),
                HttpStatusCode.Forbidden => Forbid(),
                HttpStatusCode.Conflict => Conflict(new ConflictResponse(
                    $"An existing record with the name '{createDistrictDto.Name}' was already found"
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
    [SwaggerOperation(Summary = "Cập nhật District")]
    [HttpPut]
    public async Task<ActionResult> UpdateDistrict([FromBody] UpdateDistrictDto updateDistrictDto)
    {
        try
        {
            var result = await _districtRepository.UpdateDistrict(updateDistrictDto);
            return result switch
            {
                HttpStatusCode.OK => Ok(new OkResponse()),
                HttpStatusCode.Forbidden => Forbid(),
                HttpStatusCode.NotFound => NotFound(),
                HttpStatusCode.Conflict => Conflict(new ConflictResponse(
                    $"An existing record with the name '{updateDistrictDto.Name}' was already found"
                )),
                _ => BadRequest()
            };
        }
        catch (Exception)
        {
            if (!await _districtRepository.DistrictExistsAsync(updateDistrictDto.DistrictId))
                return NotFound();

            return BadRequest();
        }
    }


    // DELETE
    [SwaggerOperation(Summary = "Xóa District")]
    [HttpDelete]
    public async Task<ActionResult> DeleteDistrict([FromBody] List<int> districtIds)
    {
        try
        {
            var result = await _districtRepository.DeleteDistrict(districtIds);
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