// using System.Net;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using semester3_real_estate_back_end.DTO.CoverArt;
// using semester3_real_estate_back_end.Interfaces;
// using semester4.DTO.CoverArt;
// using semester4.Helpers.Enums.Include;
// using semester4.Helpers.Query;
// using semester4.Interfaces;
// using semester4.Wrapper;
// using Swashbuckle.AspNetCore.Annotations;
//
// namespace semester4.Controllers;
//
// // Ảnh bìa của từng tập
// [ApiController]
// [Route("api/cover-art")]
// public class CoverArtController : ControllerBase
// {
//     private readonly ICoverArtRepository _coverArtRepository;
//
//     // GET
//     public CoverArtController(ICoverArtRepository coverArtRepository
//     )
//     {
//         _coverArtRepository = coverArtRepository;
//     }
//
//     [SwaggerOperation(Summary = "Tìm CoverArt theo điều kiện")]
//     [HttpGet]
//     public async Task<ActionResult<Response<CoverArtAndMoreDto>>> GetCoverArts([FromQuery] CoverArtQuery coverArtQuery)
//     {
//         try
//         {
//             var coverArts = await _coverArtRepository.GetCoverArts(coverArtQuery);
//             var coverArtAndMoreDtos = coverArts.Select(x => x.ConvertToCoverArtAndMoreDto()).ToList();
//             var total = coverArtAndMoreDtos.Count;
//             var limit = coverArtQuery.Limit;
//             var offset = coverArtQuery.Offset;
//             return Ok(new Response<CoverArtAndMoreDto>(coverArtAndMoreDtos, total, limit, offset));
//         }
//         catch (Exception)
//         {
//             return BadRequest();
//         }
//     }
//
//
//     // GET BY ID
//     [SwaggerOperation(Summary = "Lấy CoverArt theo ID")]
//     [HttpGet]
//     [Route("{coverArtId:guid}")]
//     public async Task<ActionResult<CoverArtAndMoreDto>> GetCoverArtById([FromRoute] Guid coverArtId,
//         [FromQuery] List<CoverArtInclude> includes)
//     {
//         try
//         {
//             var coverArt = await _coverArtRepository.GetCoverArtById(coverArtId.ToString(), includes);
//             if (coverArt == null) return NotFound();
//             return Ok(coverArt.ConvertToCoverArtAndMoreDto());
//         }
//         catch (Exception)
//         {
//             return BadRequest();
//         }
//     }
//
//
//     // POST
//     [SwaggerOperation(Summary = "Tạo CoverArt")]
//     [HttpPost]
//     [Authorize]
//     public async Task<ActionResult> CreateCoverArt([FromForm] CreateCoverArtDto createCoverArtDto)
//     {
//         try
//         {
//             var coverArt = createCoverArtDto.ConvertToCoverArt();
//             var result = await _coverArtRepository.CreateCoverArt(coverArt, createCoverArtDto.CoverArtFile);
//             return result switch
//             {
//                 HttpStatusCode.OK => Ok(new OkResponse()),
//                 HttpStatusCode.Forbidden => Forbid(),
//                 HttpStatusCode.Conflict => Conflict(),
//                 _ => BadRequest()
//             };
//         }
//         catch (Exception)
//         {
//             return BadRequest();
//         }
//     }
//
//
//     // PUT
//     [SwaggerOperation(Summary = "Cập nhật CoverArt")]
//     [HttpPut]
//     [Authorize]
//     public async Task<ActionResult> UpdateCoverArt([FromForm] UpdateCoverArtDto updateCoverArtDto)
//     {
//         try
//         {
//             var result = await _coverArtRepository.UpdateCoverArt(updateCoverArtDto);
//             return result switch
//             {
//                 HttpStatusCode.OK => Ok(new OkResponse()),
//                 HttpStatusCode.Forbidden => Forbid(),
//                 HttpStatusCode.NotFound => NotFound(),
//                 _ => BadRequest()
//             };
//         }
//         catch (Exception)
//         {
//             if (!await _coverArtRepository.CoverArtExistsAsync(updateCoverArtDto.CoverArtId.ToString()))
//                 return NotFound();
//
//             return BadRequest();
//         }
//     }
//
//
//     // DELETE
//     [SwaggerOperation(Summary = "Xóa CoverArt")]
//     [HttpDelete]
//     [Authorize]
//     public async Task<ActionResult> DeleteCoverArt([FromForm] List<Guid> coverArtIds)
//     {
//         try
//         {
//             var result = await _coverArtRepository.DeleteCoverArt(coverArtIds);
//             return result switch
//             {
//                 HttpStatusCode.OK => Ok(new OkResponse()),
//                 HttpStatusCode.Forbidden => Forbid(),
//                 HttpStatusCode.NotFound => NotFound(),
//                 _ => BadRequest()
//             };
//         }
//         catch (Exception)
//         {
//             return BadRequest();
//         }
//     }
// }