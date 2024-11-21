using System.Net;
using Microsoft.AspNetCore.Mvc;
using semester3_real_estate_back_end.DTO.Province;
using semester3_real_estate_back_end.Helpers.Query;
using semester3_real_estate_back_end.Interfaces;
using semester3_real_estate_back_end.Mapper;
using semester3_real_estate_back_end.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace semester3_real_estate_back_end.Controllers;

[ApiController]
[Route("api/province")]
public class ProvinceController : ControllerBase
{
    private readonly IProvinceRepository _provinceRepository;

    public ProvinceController(IProvinceRepository provinceRepository)
    {
        _provinceRepository = provinceRepository;
    }

    // GET
    [SwaggerOperation(Summary = "Tìm Province theo điều kiện")]
    [HttpGet]
    public async Task<ActionResult<Response<ProvinceAndMoreDto>>> GetProvinces(
        [FromQuery] ProvinceQuery provinceQuery)
    {
        var province = await _provinceRepository.GetProvinces(provinceQuery);
        var provinceAndMoreDtos = province.Select(x => x.ConvertToProvinceAndMoreDto()).ToList();
        var total = provinceAndMoreDtos.Count;
        var limit = provinceQuery.Limit;
        var offset = provinceQuery.Offset;
        return Ok(new Response<ProvinceAndMoreDto>(provinceAndMoreDtos, total, limit, offset));
    }

    // GET BY ID
    [HttpGet]
    [SwaggerOperation(Summary = "Lấy Province theo ID")]
    [Route("{provinceId:guid}")]
    public async Task<ActionResult<ProvinceAndMoreDto>> GetProvince([FromRoute] int provinceId)
    {
        try
        {
            var province = await _provinceRepository.GetProvinceById(provinceId);
            if (province == null) return NotFound();
            return Ok(province.ConvertToProvinceAndMoreDto());
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    // POST
    [SwaggerOperation(Summary = "Tạo Province")]
    [HttpPost]
    public async Task<ActionResult> CreateProvince([FromBody] CreateProvinceDto createProvinceDto)
    {
        try
        {
            var province = createProvinceDto.ConvertToProvince();
            var result = await _provinceRepository.CreateProvince(province);
            return result switch
            {
                HttpStatusCode.OK => Ok(new OkResponse()),
                HttpStatusCode.Forbidden => Forbid(),
                HttpStatusCode.Conflict => Conflict(new ConflictResponse(
                    $"An existing record with the name '{createProvinceDto.Name}' was already found"
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
    [SwaggerOperation(Summary = "Cập nhật Province")]
    [HttpPut]
    public async Task<ActionResult> UpdateProvince([FromBody] UpdateProvinceDto updateProvinceDto)
    {
        try
        {
            var result = await _provinceRepository.UpdateProvince(updateProvinceDto);
            return result switch
            {
                HttpStatusCode.OK => Ok(new OkResponse()),
                HttpStatusCode.Forbidden => Forbid(),
                HttpStatusCode.NotFound => NotFound(),
                HttpStatusCode.Conflict => Conflict(new ConflictResponse(
                    $"An existing record with the name '{updateProvinceDto.Name}' was already found"
                )),
                _ => BadRequest()
            };
        }
        catch (Exception)
        {
            if (!await _provinceRepository.ProvinceExistsAsync(updateProvinceDto.ProvinceId))
                return NotFound();

            return BadRequest();
        }
    }


    // DELETE
    [SwaggerOperation(Summary = "Xóa Province")]
    [HttpDelete]
    public async Task<ActionResult> DeleteProvince([FromBody] List<int> provinceIds)
    {
        try
        {
            var result = await _provinceRepository.DeleteProvince(provinceIds);
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