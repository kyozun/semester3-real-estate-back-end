using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using semester4.DTO.Manga;
using semester4.Helpers.Query;
using semester4.Interfaces;
using semester4.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace semester4.Controllers;

[ApiController]
[Route("api/user/follows")]
public class FollowController : ControllerBase
{
    private readonly IUserMangaRepository _userMangaRepository;

    public FollowController(IUserMangaRepository userMangaRepository)
    {
        _userMangaRepository = userMangaRepository;
    }

    // GET
    [SwaggerOperation(Summary = "Tìm Manga được follow bởi User đã đăng nhập")]
    [Route("manga")]
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<MangaAndMoreDto>> GetMangaFollowedByUser(
        [FromQuery] MangaFollowedByUserQuery mangaFollowedByUserQuery)
    {
        try
        {
            var mangas = await _userMangaRepository.GetMangaFollowedByUser(mangaFollowedByUserQuery);
            var mangaAndMoreDtos = mangas.Select(x => x.ConvertToMangaAndMoreDto()).ToList();
            var total = mangaAndMoreDtos.Count;
            var limit = mangaFollowedByUserQuery.Limit;
            var offset = mangaFollowedByUserQuery.Offset;

            var response = new Response<MangaAndMoreDto>(mangaAndMoreDtos, total, limit, offset);
            return Ok(response);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    // // GET
    // [SwaggerOperation(Summary = "Kiểm tra User đã đăng nhập đã follow Manga chưa")]
    // [Route("manga/{mangaId:guid}")]
    // [HttpGet, Authorize]
    // public async Task<ActionResult<MangaAndMoreDto>> GetMangaFollowedByUser([FromRoute] Guid mangaId)
    // {
    //     try
    //     {
    //         var mangas = await _userMangaRepository.GetMangaFollowedByUser(mangaFollowedByUserQuery);
    //         var mangaAndMoreDtos = mangas.Select(x => x.ConvertToMangaAndMoreDto()).ToList();
    //         var total = mangaAndMoreDtos.Count;
    //         var limit = mangaFollowedByUserQuery.Limit;
    //         var offset = mangaFollowedByUserQuery.Offset;
    //
    //         var response = new Response<MangaAndMoreDto>(mangaAndMoreDtos, total, limit, offset);
    //         return Ok(response);
    //     }
    //     catch (Exception)
    //     {
    //         return BadRequest();
    //     }
    // }
}