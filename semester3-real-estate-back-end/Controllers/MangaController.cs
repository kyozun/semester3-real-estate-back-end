using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using semester4.DTO.Manga;
using semester4.DTO.UserManga;
using semester4.Helpers.Enums.Include;
using semester4.Helpers.Query;
using semester4.Interfaces;
using semester4.Mapper;
using semester4.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace semester4.Controllers;

[ApiController]
[Route("api/mangas")]
public class MangaController : ControllerBase
{
    private readonly IMangaRepository _mangaRepository;
    private readonly IUserMangaRepository _userMangaRepository;

    public MangaController(IMangaRepository mangaRepository,
        IUserMangaRepository userMangaRepository)
    {
        _mangaRepository = mangaRepository;
        _userMangaRepository = userMangaRepository;
    }

    // GET
    [SwaggerOperation(Summary = "Tìm Manga theo điều kiện")]
    [HttpGet]
    public async Task<ActionResult<Response<MangaAndMoreDto>>> GetMangas([FromQuery] MangaQuery mangaQuery)
    {
        try
        {
            var mangas = await _mangaRepository.GetMangas(mangaQuery);
            var mangaAndMoreDtos = mangas.Select(x => x.ConvertToMangaAndMoreDto()).ToList();
            var total = mangaAndMoreDtos.Count;
            var limit = mangaQuery.Limit;
            var offset = mangaQuery.Offset;

            var response = new Response<MangaAndMoreDto>(mangaAndMoreDtos, total, limit, offset);
            return Ok(response);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    // GET BY ID
    [HttpGet]
    [SwaggerOperation(Summary = "Tìm Manga theo ID")]
    [Route("{mangaId:guid}")]
    public async Task<ActionResult<MangaAndMoreDto>> GetManga([FromRoute] Guid mangaId,
        [FromQuery] List<MangaInclude> includes)
    {
        try
        {
            var manga = await _mangaRepository.GetMangaById(mangaId.ToString(), includes);
            if (manga == null) return NotFound();
            return Ok(manga.ConvertToMangaAndMoreDto());
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    // POST
    [SwaggerOperation(Summary = "Tạo Manga")]
    [HttpPost]
    [Authorize]
    public async Task<ActionResult> CreateManga([FromBody] CreateMangaDto createMangaDto)
    {
        try
        {
            var manga = createMangaDto.ConvertToManga();
            var result = await _mangaRepository.CreateManga(manga);
            return result switch
            {
                HttpStatusCode.OK => Ok(new OkResponse()),
                HttpStatusCode.Forbidden => Forbid(),
                HttpStatusCode.Conflict => Conflict(new ConflictResponse(
                    $"An existing record with the title '{createMangaDto.Title}' was already found"
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
    [SwaggerOperation(Summary = "Cập nhật Manga")]
    [HttpPut]
    [Authorize]
    public async Task<ActionResult> UpdateManga([FromBody] UpdateMangaDto updateMangaDto)
    {
        try
        {
            var result = await _mangaRepository.UpdateManga(updateMangaDto);
            return result switch
            {
                HttpStatusCode.OK => Ok(new OkResponse()),
                HttpStatusCode.Forbidden => Forbid(),
                HttpStatusCode.NotFound => NotFound(),
                HttpStatusCode.Conflict => Conflict(new ConflictResponse(
                    $"An existing record with the title '{updateMangaDto.Title}' was already found"
                )),
                _ => BadRequest()
            };
        }
        catch (Exception)
        {
            if (!await _mangaRepository.MangaExistsAsync(updateMangaDto.MangaId.ToString()!)) return NotFound();

            return BadRequest();
        }
    }

    // DELETE
    [SwaggerOperation(Summary = "Xóa Manga")]
    [HttpDelete]
    [Authorize]
    public async Task<ActionResult> DeleteManga([FromBody] List<Guid> mangaIds)
    {
        try
        {
            var result = await _mangaRepository.DeleteManga(mangaIds);
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


    // POST 
    [SwaggerOperation(Summary = "Theo dõi Manga")]
    [HttpPost]
    [Authorize]
    [Route("{mangaId:guid}/follow")]
    public async Task<ActionResult> FollowManga([FromRoute] Guid mangaId)
    {
        try
        {
            var result = await _mangaRepository.FollowManga(mangaId.ToString());
            return result switch
            {
                HttpStatusCode.OK => Ok(new OkResponse()),
                HttpStatusCode.Forbidden => Forbid(),
                HttpStatusCode.Conflict => Conflict(new ConflictResponse(
                    "Already following this manga"
                )),

                _ => BadRequest()
            };
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    // DELETE
    [SwaggerOperation(Summary = "Hủy theo dõi Manga")]
    [HttpDelete]
    [Authorize]
    [Route("{mangaId:guid}/follow")]
    public async Task<ActionResult> UnFollowManga([FromRoute] Guid mangaId)
    {
        try
        {
            var result = await _mangaRepository.UnFollowManga(mangaId.ToString());
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

    // GET
    [SwaggerOperation(Summary = "Tìm Manga Reading Status của người dùng đã đăng nhập ")]
    [HttpGet]
    [Authorize]
    [Route("status")]
    public async Task<ActionResult> GetAllMangaReadingStatus(
        [FromQuery] MangaReadingStatusQuery mangaReadingStatusQuery)
    {
        try
        {
            var userMangas = await _userMangaRepository.GetMangaReadingStatus(mangaReadingStatusQuery);
            var mangaReadingStatusDtos = userMangas.Select(x => x.ConvertToMangaReadingStatusDto()).ToList();
            var total = mangaReadingStatusDtos.Count;
            var limit = mangaReadingStatusQuery.Limit;
            var offset = mangaReadingStatusQuery.Offset;
            return Ok(new Response<MangaReadingStatusDto>(mangaReadingStatusDtos, total, limit, offset));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }


    // UPDATE
    [SwaggerOperation(Summary = "Cập nhật MangaReadingStatus của Manga đang theo dõi")]
    [HttpPut]
    [Authorize]
    [Route("{mangaId:guid}/status")]
    public async Task<ActionResult> UpdateMangaReadingStatus([FromRoute] Guid mangaId,
        [FromBody] UpdateMangaReadingStatusDto updateMangaReadingStatusDto)
    {
        try
        {
            var result =
                await _userMangaRepository.UpdateMangaReadingStatus(mangaId.ToString(), updateMangaReadingStatusDto);
            return result switch
            {
                HttpStatusCode.OK => Ok(new OkResponse()),
                HttpStatusCode.Forbidden => Forbid(),
                HttpStatusCode.NotFound => NotFound(),
                HttpStatusCode.Conflict => Conflict(),
                _ => BadRequest()
            };
        }
        catch (Exception)
        {
            if (!await _mangaRepository.MangaExistsAsync(mangaId.ToString())) return NotFound();

            return BadRequest();
        }
    }


    // GET
    [SwaggerOperation(Summary = "Tim Manga cùng thể loại")]
    [HttpGet]
    [Route("{mangaId:guid}/relation")]
    public async Task<ActionResult<Response<MangaAndMoreDto>>> GetRelationMangas([FromRoute] Guid mangaId,
        [FromQuery] MangaQuery mangaQuery)
    {
        var mangas = await _mangaRepository.GetRelationMangas(mangaId.ToString(), mangaQuery);
        if (mangas == null) return NotFound();

        var mangaAndMoreDtos = mangas.Select(x => x.ConvertToMangaAndMoreDto()).ToList();
        var total = mangaAndMoreDtos.Count;
        var limit = mangaQuery.Limit;
        var offset = mangaQuery.Offset;

        var response = new Response<MangaAndMoreDto>(mangaAndMoreDtos, total, limit, offset);
        return Ok(response);
    }

    // GET
    [SwaggerOperation(Summary = "Tìm Manga bất kỳ")]
    [HttpGet]
    [Route("random")]
    public async Task<ActionResult<Response<MangaAndMoreDto>>> GetRandomManga([FromQuery] MangaQuery mangaQuery)
    {
        try
        {
            var manga = await _mangaRepository.GetRandomManga(mangaQuery);
            if (manga == null) return NotFound();
            return Ok(manga.ConvertToMangaAndMoreDto());
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}