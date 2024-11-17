using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using semester4.DTO.CustomList;
using semester4.DTO.CustomListManga;
using semester4.Helpers.Query;
using semester4.Interfaces;
using semester4.Mapper;
using semester4.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace semester4.Controllers;

[ApiController]
[Route("api")]
public class CustomListController : ControllerBase
{
    private readonly ICustomListRepository _customListRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CustomListController(ICustomListRepository customListRepository, IHttpContextAccessor httpContextAccessor)
    {
        _customListRepository = customListRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    // GET
    [SwaggerOperation(Summary = "Tìm Custom List")]
    [HttpGet]
    [Route("custom-list")]
    public async Task<ActionResult<Response<CustomListAndMoreDto>>> GetCustomLists(
        [FromQuery] CustomListQuery customListQuery)
    {
        try
        {
            var customLists = await _customListRepository.GetCustomLists(customListQuery);
            var customListAndMoreDtos = customLists.Select(x => x.ConvertToCustomListAndMoreDto()).ToList();
            var total = customListAndMoreDtos.Count;
            var limit = customListQuery.Limit;
            var offset = customListQuery.Offset;
            return Ok(new Response<CustomListAndMoreDto>(customListAndMoreDtos, total, limit, offset));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    // GET BY ID
    [SwaggerOperation(Summary = "Lấy Custom List theo ID")]
    [HttpGet]
    [Route("custom-list/{customListId:guid}")]
    public async Task<ActionResult<CustomListAndMoreDto>> GetCustomList([FromRoute] Guid customListId)
    {
        try
        {
            var customList = await _customListRepository.GetCustomListById(customListId.ToString());
            if (customList == null) return NotFound();

            return Ok(customList.ConvertToCustomListAndMoreDto());
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    // POST
    [SwaggerOperation(Summary = "Tạo Custom List")]
    [HttpPost]
    [Authorize]
    [Route("custom-list")]
    public async Task<ActionResult> CreateCustomList([FromBody] CreateCustomListDto createCustomListDto)
    {
        try
        {
            var userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

            var customList = createCustomListDto.ConvertToCustomList();
            customList.UserId = userId;
            await _customListRepository.CreateCustomList(customList);

            return Ok(new OkResponse());
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    // PUT
    [SwaggerOperation(Summary = "Cập nhật Custom List")]
    [HttpPut]
    [Route("custom-list/{customListId:guid}")]
    public async Task<ActionResult> UpdateCustomList([FromRoute] Guid customListId,
        [FromBody] UpdateCustomListDto updateCustomListDto)
    {
        try
        {
            var customList = await _customListRepository.UpdateCustomList(customListId.ToString(), updateCustomListDto);
            if (customList == null) return NotFound();

            return Ok(new OkResponse());
        }
        catch (Exception)
        {
            if (!await _customListRepository.CustomListExistsAsync(customListId.ToString())) return NotFound();
            return BadRequest();
        }
    }

    // DELETE
    [SwaggerOperation(Summary = "Xóa Custom List")]
    [HttpDelete]
    [Route("custom-list/{customListId:guid}")]
    public async Task<ActionResult> DeleteCustomList([FromRoute] Guid customListId)
    {
        var customList = await _customListRepository.DeleteCustomList(customListId.ToString());
        if (customList == null) return NotFound();

        return Ok(new OkResponse());
    }

    // GET
    [SwaggerOperation(Summary = "Tìm Custom List của User đã đăng nhập")]
    [HttpGet]
    [Authorize]
    [Route("user/custom-list")]
    public async Task<ActionResult<Response<CustomListAndMoreDto>>> GetCustomListsForLoggedUser()
    {
        var userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var customLists = await _customListRepository.GetCustomListsForLoggedUser(userId);
        var customListAndMoreDtos = customLists.Select(x => x.ConvertToCustomListAndMoreDto()).ToList();
        var total = customListAndMoreDtos.Count;
        // var limit = customListQuery.Limit;
        // var offset = customListQuery.Offset;
        return Ok(new Response<CustomListAndMoreDto>(customListAndMoreDtos, total, 0, 0));
    }

    // GET
    [SwaggerOperation(Summary = "Tìm Custom List của User theo userID")]
    [HttpGet]
    [Authorize]
    [Route("user/{userId:alpha}/custom-list")]
    public async Task<ActionResult<Response<CustomListAndMoreDto>>> GetCustomListByUserId(string userId)
    {
        var customLists = await _customListRepository.GetCustomListByUserId(userId);
        var customListAndMoreDtos = customLists.Select(x => x.ConvertToCustomListAndMoreDto()).ToList();
        var total = customListAndMoreDtos.Count;
        return Ok(new Response<CustomListAndMoreDto>(customListAndMoreDtos, total, 0, 0));
    }

    // POST
    [SwaggerOperation(Summary = "Thêm manga vào Custom List")]
    [HttpPost]
    [Authorize]
    [Route("manga/custom-list")]
    public async Task<ActionResult> CreateMangaToCustomList(
        [FromBody] CreateCustomListMangaDto createCustomListMangaDto)
    {
        try
        {
            var userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            foreach (var mangaId in createCustomListMangaDto.MangaIds)
            {
                var customListManga = createCustomListMangaDto.ConvertToCustomListManga();
                customListManga.MangaId = mangaId;
                await _customListRepository.CreateCustomListManga(userId, customListManga);
            }

            return Ok(new OkResponse());
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    // DELETE
    [SwaggerOperation(Summary = "Xóa manga khỏi Custom List")]
    [HttpDelete]
    [Authorize]
    [Route("manga/custom-list")]
    public async Task<ActionResult> DeleteMangaFromCustomList(
        [FromBody] CreateCustomListMangaDto createCustomListMangaDto)
    {
        try
        {
            var userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var customListManga = createCustomListMangaDto.ConvertToCustomListManga();
            await _customListRepository.CreateCustomListManga(userId, customListManga);
            return Ok(new OkResponse());
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}