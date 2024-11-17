using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using semester4.DTO.Rating;
using semester4.Helpers.Enums.Include;
using semester4.Helpers.Query;
using semester4.Interfaces;
using semester4.Mapper;
using semester4.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace semester4.Controllers;

[ApiController]
[Route("api/ratings")]
public class RatingController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IRatingRepository _ratingRepository;

    public RatingController(IRatingRepository ratingRepository, IHttpContextAccessor httpContextAccessor)
    {
        _ratingRepository = ratingRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    // GET
    [SwaggerOperation(Summary = "Lấy Rating của 1 hoặc nhiều Manga")]
    [HttpGet]
    public async Task<ActionResult<Response<RatingAndMoreDto>>> GetRatings([FromQuery] RatingQuery ratingQuery)
    {
        try
        {
            var ratings = await _ratingRepository.GetRatings(ratingQuery);
            var ratingAndMoreDtos = ratings.Select(x => x.ConvertToRatingAndMoreDto()).ToList();
            var total = ratingAndMoreDtos.Count;
            var limit = ratingQuery.Limit;
            var offset = ratingQuery.Offset;
            return Ok(new Response<RatingAndMoreDto>(ratingAndMoreDtos, total, limit, offset));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    // GET BY ID
    [HttpGet]
    [SwaggerOperation(Summary = "Lấy Rating của 1 Manga")]
    [Route("{ratingId:guid}")]
    public async Task<ActionResult<RatingAndMoreDto>> GetRating([FromRoute] Guid ratingId,
        [FromQuery] List<RatingInclude> includes)
    {
        try
        {
            var rating = await _ratingRepository.GetRatingById(ratingId.ToString(), includes);
            if (rating == null) return NotFound();
            return Ok(rating.ConvertToRatingAndMoreDto());
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    // POST
    [SwaggerOperation(Summary = "Tạo Rating")]
    [HttpPost]
    [Authorize]
    public async Task<ActionResult> CreateRating([FromForm] CreateRatingDto createRatingDto)
    {
        try
        {
            var rating = createRatingDto.ConvertToRating();
            var result = await _ratingRepository.CreateRating(rating);
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
    [SwaggerOperation(Summary = "Cập nhật Rating")]
    [HttpPut]
    [Authorize]
    public async Task<ActionResult> UpdateRating([FromForm] UpdateRatingDto updateRatingDto)
    {
        try
        {
            var result = await _ratingRepository.UpdateRating(updateRatingDto);
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
            if (!await _ratingRepository.RatingExistsAsync(updateRatingDto.RatingId.ToString()!)) return NotFound();

            return BadRequest();
        }
    }

    // DELETE
    [SwaggerOperation(Summary = "Xóa Rating")]
    [HttpDelete]
    [Authorize]
    public async Task<ActionResult> DeleteRating([FromForm] [Required] List<Guid> ratingIds)
    {
        try
        {
            var result = await _ratingRepository.DeleteRating(ratingIds);
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