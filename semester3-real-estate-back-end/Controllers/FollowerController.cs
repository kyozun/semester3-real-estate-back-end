// using Microsoft.AspNetCore.Mvc;
// using semester4.DTO.Follower;
// using semester4.Helpers;
// using semester4.Interfaces;
// using semester4.Mapper;
// using semester4.Wrapper;
//
// namespace semester4.Controllers;
//
// [ApiController]
// [Route("api/followers")]
// public class FollowerController : ControllerBase
// {
//      private readonly IFollowerRepository _followerRepository;
//
//     public FollowerController(IFollowerRepository followerRepository)
//     {
//         _followerRepository = followerRepository;
//     }
//
//     // GET
//     [HttpGet]
//     public async Task<ActionResult<Response<FollowerAndMoreDto>>> GetFollowers([FromQuery] FollowerQuery followerQuery)
//     {
//         var followers = await _followerRepository.GetFollowers(followerQuery);
//         var followerAndMoreDtos = followers.Select(x => x.ConvertToFollowerAndMoreDto()).ToList();
//         var total = followerAndMoreDtos.Count;
//         return Ok(new Response<FollowerAndMoreDto>(followerAndMoreDtos, total));
//     }
//
//     // GET BY ID
//     [HttpGet]
//     [Route("{id:int}")]
//     public async Task<ActionResult<FollowerAndMoreDto>> GetFollower([FromRoute] int id, [FromQuery] string include = "")
//     {
//         var follower = await _followerRepository.GetFollowerById(id, include);
//         if (follower == null) return NotFound();
//
//         return Ok(follower.ConvertToFollowerAndMoreDto());
//     }
//
//     // POST
//     [HttpPost]
//     public async Task<ActionResult> CreateFollower([FromBody] CreateFollowerDto createFollowerDto)
//     {
//         try
//         {
//             var follower = createFollowerDto.ConvertToFollower();
//             await _followerRepository.CreateFollower(follower);
//             return Ok(new OkResponse());
//         }
//         catch (Exception)
//         {
//             return BadRequest();
//         }
//     }
//     
//     // PUT
//     [HttpPut]
//     [Route("{id:int}")]
//     public async Task<ActionResult> UpdateFollower([FromRoute] int id, [FromBody] UpdateFollowerDto updateFollowerDto)
//     {
//         var follower = await _followerRepository.UpdateFollower(id, updateFollowerDto);
//         if (follower == null) return NotFound();
//
//         return Ok(new OkResponse());
//     }
//     
//     // DELETE (Unfollow manga)
//     [HttpDelete]
//     public async Task<ActionResult> DeleteFollower([FromBody] DeleteFollowerDto deleteFollowerDto)
//     {
//         var follower = await _followerRepository.DeleteFollower(deleteFollowerDto);
//         if (follower == null) return NotFound();
//
//         return Ok(new OkResponse());
//     }
// }

