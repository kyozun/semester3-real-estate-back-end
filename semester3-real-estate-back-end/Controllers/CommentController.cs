// using System.Security.Claims;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using semester4.DTO.Comment;
// using semester4.Helpers;
// using semester4.Helpers.Query;
// using semester4.Interfaces;
// using semester4.Mapper;
// using semester4.Wrapper;
// using Swashbuckle.AspNetCore.Annotations;
//
// namespace semester4.Controllers;
//
// [ApiController]
// [Route("api/mangas")]
// public class CommentController : ControllerBase
// {
//     private readonly ICommentRepository _commentRepository;
//     private readonly IHttpContextAccessor _httpContextAccessor;
//
//     public CommentController(ICommentRepository commentRepository, IHttpContextAccessor httpContextAccessor
//     )
//     {
//         _commentRepository = commentRepository;
//         _httpContextAccessor = httpContextAccessor;
//     }
//
//     // GET
//     [SwaggerOperation(Summary = "ADMIN - Tìm comment của manga")]
//     [HttpGet]
//     [Route("comments")]
//     public async Task<ActionResult<Response<CommentAndMoreDto>>> GetComments([FromQuery] CommentQuery commentQuery)
//     {
//         var comments = await _commentRepository.GetComments(commentQuery);
//         var commentAndMoreDtos = comments.Select(x => x.ConvertToCommentAndMoreDto()).ToList();
//         var total = commentAndMoreDtos.Count;
//         var limit = commentQuery.Limit;
//         var offset = commentQuery.Offset;
//         return Ok(new Response<CommentAndMoreDto>(commentAndMoreDtos, total, limit, offset));
//     }
//
//
//     // GET BY ID (Chỉ admin mới edit comment)
//     [SwaggerOperation(Summary = "ADMIN - Lấy comment theo commentId")]
//     [HttpGet, Authorize(Policy = "EditCommentPolicy")]
//     [Route("comments/{commentId:guid}")]
//     public async Task<ActionResult<CommentAndMoreDto>> GetCommentById([FromRoute] Guid commentId,
//         [FromQuery] string include = "")
//     {
//         var comment = await _commentRepository.GetCommentById(commentId.ToString(), include);
//         if (comment == null) return NotFound();
//
//         return Ok(comment.ConvertToCommentAndMoreDto());
//     }
//
//     // GET BY ID
//     [SwaggerOperation(Summary = "Lấy comment của manga theo mangaId")]
//     [HttpGet]
//     [Route("{mangaId:guid}/comments")]
//     public async Task<ActionResult<CommentAndMoreDto>> GetCommentsByMangaId([FromRoute] Guid mangaId)
//     {
//         var comments = await _commentRepository.GetCommentsByMangaId(mangaId.ToString());
//         var commentAndMoreDtos = comments.Select(x => x.ConvertToCommentAndMoreDto()).ToList();
//         var total = commentAndMoreDtos.Count;
//         return Ok(new Response<CommentAndMoreDto>(commentAndMoreDtos, total, 0, 0));
//     }
//
//     // GET BY ID
//     [SwaggerOperation(Summary = "ADMIN - Lấy comment của User theo userId")]
//     [HttpGet]
//     [Route("users/{userId:guid}/comments")]
//     public async Task<ActionResult<CommentAndMoreDto>> GetCommentsByUserId([FromRoute] string userId)
//     {
//         var comments = await _commentRepository.GetCommentsByUserId(userId);
//         var commentAndMoreDtos = comments.Select(x => x.ConvertToCommentAndMoreDto()).ToList();
//         var total = commentAndMoreDtos.Count;
//         return Ok(new Response<CommentAndMoreDto>(commentAndMoreDtos, total, 0,0));
//     }
//     
//     // POST
//     [SwaggerOperation(Summary = "Tạo comment cho manga")]
//     [HttpPost, Authorize]
//     [Route("comments")]
//     public async Task<ActionResult> CreateComment([FromBody] CreateCommentDto createCommentDto)
//     {
//         try
//         {
//             var userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
//             var comment = createCommentDto.ConvertToComment(userId);
//             await _commentRepository.CreateComment(comment);
//             return Ok(new OkResponse());
//         }
//         catch (Exception)
//         {
//             return BadRequest();
//         }
//     }
//
//
//     // PUT
//     [SwaggerOperation(Summary = "Cập nhật comment của manga")]
//     [HttpPut, Authorize(Policy = "EditCommentPolicy")]
//     [Route("comments/{commentId:guid}")]
//     public async Task<ActionResult> UpdateComment([FromRoute] Guid commentId,
//         [FromBody] UpdateCommentDto updateCommentDto, [FromQuery] string include = "")
//     {
//         // Tìm comment theo commentId, nếu ko có trả về NotFound
//         var comment = await _commentRepository.GetCommentById(commentId.ToString(), include);
//         if (comment == null)
//         {
//             return NotFound();
//         }
//
//         // Nếu user không phải là chủ sở hữu của comment thì báo lỗi
//         var userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
//         if (comment.UserId != userId)
//         {
//             return Forbid();
//         }
//
//
//         if (updateCommentDto.Name == null) return Ok(new OkResponse());
//         comment.Name = updateCommentDto.Name;
//         try
//         {
//             await _commentRepository.UpdateComment(comment);
//         }
//         catch
//         {
//             if (!await _commentRepository.CommentExistsAsync(commentId.ToString()))
//             {
//                 return NotFound();
//             }
//
//             return BadRequest();
//         }
//
//
//         return Ok(new OkResponse());
//     }
//
//     // DELETE
//     [SwaggerOperation(Summary = "ADMIN - Xóa comment của manga")]
//     [HttpDelete, Authorize]
//     [Route("comments/{commentId:guid}")]
//     public async Task<ActionResult> DeleteComment([FromRoute] Guid commentId)
//     {
//         var userId = _httpContextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
//         var comment = await _commentRepository.DeleteComment(commentId.ToString(), userId);
//         if (comment == null) return NotFound();
//
//         return Ok(new OkResponse());
//     }
//
//    
// }

