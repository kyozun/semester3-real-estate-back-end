using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using semester4.DTO.Chapter;
using semester4.Helpers.Enums.Include;
using semester4.Helpers.Query;
using semester4.Interfaces;
using semester4.Mapper;
using semester4.Wrapper;
using Swashbuckle.AspNetCore.Annotations;

namespace semester4.Controllers;

[ApiController]
[Route("api/chapters")]
public class ChapterController : ControllerBase
{
    private readonly IChapterRepository _chapterRepository;

    public ChapterController(IChapterRepository chapterRepository
    )
    {
        _chapterRepository = chapterRepository;
    }

    [SwaggerOperation(Summary = "Tìm Chapter theo điều kiện")]
    [HttpGet]
    public async Task<ActionResult<Response<ChapterAndMoreDto>>> GetChapters([FromQuery] ChapterQuery chapterQuery)
    {
        try
        {
            var chapters = await _chapterRepository.GetChapters(chapterQuery);
            var chapterAndMoreDtos = chapters.Select(x => x.ConvertToChapterAndMoreDto()).ToList();
            var total = chapterAndMoreDtos.Count;
            var limit = chapterQuery.Limit;
            var offset = chapterQuery.Offset;
            return Ok(new Response<ChapterAndMoreDto>(chapterAndMoreDtos, total, limit, offset));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }


    // GET BY ID
    [SwaggerOperation(Summary = "Lấy Chapter theo ID")]
    [HttpGet]
    [Route("{chapterId:guid}")]
    public async Task<ActionResult<ChapterAndMoreDto>> GetChapterById([FromRoute] Guid chapterId,
        [FromQuery] List<ChapterInclude> includes)
    {
        try
        {
            var chapter = await _chapterRepository.GetChapterById(chapterId.ToString(), includes);
            if (chapter == null) return NotFound();
            return Ok(chapter.ConvertToChapterAndMoreDto());
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }


    // POST
    [SwaggerOperation(Summary = "Tạo Chapter")]
    [HttpPost]
    [Authorize]
    public async Task<ActionResult> CreateChapter([FromBody] CreateChapterDto createChapterDto)
    {
        try
        {
            var chapter = createChapterDto.ConvertToChapter();
            var result = await _chapterRepository.CreateChapter(chapter);
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
    [SwaggerOperation(Summary = "Cập nhật Chapter")]
    [HttpPut]
    [Authorize]
    public async Task<ActionResult> UpdateChapter([FromBody] UpdateChapterDto updateChapterDto)
    {
        try
        {
            var result = await _chapterRepository.UpdateChapter(updateChapterDto);
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
            if (!await _chapterRepository.ChapterExistsAsync(updateChapterDto.ChapterId.ToString())) return NotFound();

            return BadRequest();
        }
    }


    // DELETE
    [SwaggerOperation(Summary = "Xóa Chapter")]
    [HttpDelete]
    [Authorize]
    public async Task<ActionResult> DeleteChapter([FromBody] List<Guid> chapterIds)
    {
        try
        {
            var result = await _chapterRepository.DeleteChapter(chapterIds);
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