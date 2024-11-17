using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using semester4.DTO.ChapterComment;
using semester4.DTO.Comment;
using semester4.Helpers.Query;
using semester4.Interfaces;
using semester4.Mapper;
using semester4.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace semester4.Controllers;

[ApiController]
[Route("api/chapters")]
public class ChapterCommentController : ControllerBase
{
    private readonly IChapterCommentRepository _chapterCommentRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ChapterCommentController(IChapterCommentRepository chapterCommentRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _chapterCommentRepository = chapterCommentRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    // GET
    [SwaggerOperation(Summary = "ADMIN - Tìm comment của chapter")]
    [HttpGet]
    [Route("comments")]
    public async Task<ActionResult<Response<CommentAndMoreDto>>> GetChapterComments(
        [FromQuery] ChapterCommentQuery chapterCommentQuery)
    {
        var chapterComments = await _chapterCommentRepository.GetChapterComments(chapterCommentQuery);
        var chapterCommentAndMoreDtos = chapterComments.Select(x => x.ConvertToChapterCommentAndMoreDto()).ToList();
        var total = chapterCommentAndMoreDtos.Count;
        var limit = chapterCommentQuery.Limit;
        var offset = chapterCommentQuery.Offset;
        return Ok(new Response<ChapterCommentAndMoreDto>(chapterCommentAndMoreDtos, total, limit, offset));
    }

    // GET BY ID (Chỉ admin mới edit comment)
    [SwaggerOperation(Summary = "ADMIN - Lấy comment theo chapterCommentId")]
    [HttpGet]
    [Route("comments/{chapterCommentId:guid}")]
    public async Task<ActionResult<CommentAndMoreDto>> GetChapterCommentById([FromRoute] Guid chapterCommentId)
    {
        var chapterComment = await _chapterCommentRepository.GetChapterCommentById(chapterCommentId.ToString());
        if (chapterComment == null) return NotFound();

        return Ok(chapterComment.ConvertToChapterCommentAndMoreDto());
    }

    // POST
    [SwaggerOperation(Summary = "Tạo comment cho chapter")]
    [HttpPost]
    [Authorize]
    [Route("comments")]
    public async Task<ActionResult> CreateComment([FromBody] CreateChapterCommentDto createChapterCommentDto)
    {
        try
        {
            var userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var chapterComment = createChapterCommentDto.ConvertToChapterComment(userId);
            await _chapterCommentRepository.CreateChapterComment(chapterComment);
            return Ok(new OkResponse());
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    // PUT
    [SwaggerOperation(Summary = "Cập nhật comment của chapter")]
    [HttpPut]
    [Authorize]
    [Route("comments/{chapterCommentId:guid}")]
    public async Task<ActionResult> UpdateComment([FromRoute] Guid chapterCommentId,
        [FromBody] UpdateCommentDto updateCommentDto, [FromQuery] string include = "")
    {
        // Tìm comment theo chapterCommentId, nếu ko có trả về NotFound
        var chapterComment = await _chapterCommentRepository.GetChapterCommentById(chapterCommentId.ToString());
        if (chapterComment == null) return NotFound();

        // Nếu user không phải là chủ sở hữu của chapterComment thì báo lỗi
        var userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        if (chapterComment.UserId != userId) return Forbid();

        if (updateCommentDto.Name == null) return Ok(new OkResponse());
        chapterComment.Name = updateCommentDto.Name;
        try
        {
            await _chapterCommentRepository.UpdateChapterComment(chapterComment);
        }
        catch
        {
            // Neu cap nhat khong thanh cong
            if (!await _chapterCommentRepository.ChapterCommentExistsAsync(chapterCommentId.ToString()))
                return NotFound();

            return BadRequest();
        }


        return Ok(new OkResponse());
    }

    // DELETE
    [SwaggerOperation(Summary = "ADMIN - Xóa comment của manga")]
    [HttpDelete]
    [Authorize]
    [Route("comments/{chapterCommentId:guid}")]
    public async Task<ActionResult> DeleteComment([FromRoute] Guid chapterCommentId)
    {
        var userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var comment = await _chapterCommentRepository.DeleteChapterComment(chapterCommentId.ToString(), userId);
        if (comment == null) return NotFound();

        return Ok(new OkResponse());
    }

    // GET BY ID
    [SwaggerOperation(Summary = "Lấy comment của chapter theo chapterId")]
    [HttpGet]
    [Route("{chapterId:guid}/comments")]
    public async Task<ActionResult<ChapterCommentAndMoreDto>> GetCommentsByMangaId([FromRoute] Guid chapterId)
    {
        var chapterComments = await _chapterCommentRepository.GetChapterCommentsByChapterId(chapterId.ToString());
        var chapterCommentAndMoreDtos = chapterComments.Select(x => x.ConvertToChapterCommentAndMoreDto()).ToList();
        var total = chapterCommentAndMoreDtos.Count;
        return Ok(new Response<ChapterCommentAndMoreDto>(chapterCommentAndMoreDtos, total, 0, 0));
    }

    // GET BY ID
    [SwaggerOperation(Summary = "ADMIN - Lấy comment của User theo userId")]
    [HttpGet]
    [Route("users/{userId:guid}/comments")]
    public async Task<ActionResult<ChapterCommentAndMoreDto>> GetCommentsByUserId([FromRoute] Guid userId)
    {
        var chapterComments = await _chapterCommentRepository.GetChapterCommentsByUserId(userId.ToString());
        var chapterCommentAndMoreDtos = chapterComments.Select(x => x.ConvertToChapterCommentAndMoreDto()).ToList();
        var total = chapterCommentAndMoreDtos.Count;
        return Ok(new Response<ChapterCommentAndMoreDto>(chapterCommentAndMoreDtos, total, 0, 0));
    }
}