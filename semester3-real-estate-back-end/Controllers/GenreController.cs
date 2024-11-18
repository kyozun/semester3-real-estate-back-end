// using System.Net;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using semester4.DTO.Genre;
// using semester4.Helpers.Enums.Include;
// using semester4.Helpers.Query;
// using semester4.Interfaces;
// using semester4.Wrapper;
// using Swashbuckle.AspNetCore.Annotations;
//
// namespace semester4.Controllers;
//
// [ApiController]
// [Route("api/genres")]
// public class GenreController : ControllerBase
// {
//     private readonly IGenreRepository _genreRepository;
//
//     public GenreController(IGenreRepository genreRepository)
//     {
//         _genreRepository = genreRepository;
//     }
//
//     // GET
//     [SwaggerOperation(Summary = "Tìm thể loại")]
//     [HttpGet]
//     public async Task<ActionResult<Response<GenreAndMoreDto>>> GetGenres([FromQuery] GenreQuery genreQuery)
//     {
//         try
//         {
//             var genres = await _genreRepository.GetGenres(genreQuery);
//             var genreAndMoreDtos = genres.Select(x => x.ConvertToGenreAndMoreDto()).ToList();
//             var total = genreAndMoreDtos.Count;
//             var limit = genreQuery.Limit;
//             var offset = genreQuery.Offset;
//             return Ok(new Response<GenreAndMoreDto>(genreAndMoreDtos, total, limit, offset));
//         }
//         catch (Exception)
//         {
//             return BadRequest();
//         }
//     }
//
//     // GET BY ID
//     [HttpGet]
//     [SwaggerOperation(Summary = "Lấy Genre của 1 Manga")]
//     [Route("{genreId:guid}")]
//     public async Task<ActionResult<GenreAndMoreDto>> GetGenre([FromRoute] Guid genreId,
//         [FromQuery] List<GenreInclude> includes)
//     {
//         try
//         {
//             var genre = await _genreRepository.GetGenreById(genreId.ToString(), includes);
//             if (genre == null) return NotFound();
//             return Ok(genre.ConvertToGenreAndMoreDto());
//         }
//         catch (Exception)
//         {
//             return BadRequest();
//         }
//     }
//
//     // POST
//     [SwaggerOperation(Summary = "Tạo thể loại")]
//     [HttpPost]
//     [Authorize]
//     public async Task<ActionResult> CreateGenre([FromBody] CreateGenreDto createGenreDto)
//     {
//         try
//         {
//             var genre = createGenreDto.ConvertToGenre();
//             var result = await _genreRepository.CreateGenre(genre);
//             return result switch
//             {
//                 HttpStatusCode.OK => Ok(new OkResponse()),
//                 HttpStatusCode.Forbidden => Forbid(),
//                 HttpStatusCode.Conflict => Conflict(new ConflictResponse(
//                     $"An existing record with the name '{createGenreDto.Name}' was already found"
//                 )),
//
//                 _ => BadRequest()
//             };
//         }
//         catch (Exception)
//         {
//             return BadRequest();
//         }
//     }
//
//     // PUT
//     [SwaggerOperation(Summary = "Cập nhật the loai")]
//     [HttpPut]
//     [Authorize]
//     public async Task<ActionResult> UpdateGenre([FromBody] UpdateGenreDto updateGenreDto)
//     {
//         try
//         {
//             var result = await _genreRepository.UpdateGenre(updateGenreDto);
//             return result switch
//             {
//                 HttpStatusCode.OK => Ok(new OkResponse()),
//                 HttpStatusCode.Forbidden => Forbid(),
//                 HttpStatusCode.NotFound => NotFound(),
//                 HttpStatusCode.Conflict => Conflict(new ConflictResponse(
//                     $"An existing record with the name '{updateGenreDto.Name}' was already found"
//                 )),
//                 _ => BadRequest()
//             };
//         }
//         catch (Exception)
//         {
//             if (!await _genreRepository.GenreExistsAsync(updateGenreDto.GenreId.ToString()!)) return NotFound();
//
//             return BadRequest();
//         }
//     }
//
//
//     // DELETE
//     [SwaggerOperation(Summary = "Xóa thể loại")]
//     [HttpDelete]
//     [Authorize]
//     public async Task<ActionResult> DeleteGenre([FromBody] List<Guid> genreIds)
//     {
//         try
//         {
//             var result = await _genreRepository.DeleteGenre(genreIds);
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