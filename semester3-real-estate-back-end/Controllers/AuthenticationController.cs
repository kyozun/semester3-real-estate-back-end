using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using semester3_real_estate_back_end.DTO.User;
using semester3_real_estate_back_end.Interfaces;
using semester3_real_estate_back_end.Models;
using semester3_real_estate_back_end.Wrapper;
using semester4.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace semester3_real_estate_back_end.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly UserManager<User> _userManager;

    public AuthenticationController(UserManager<User> userManager, ITokenService tokenService,
        SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }


    // Register
    [SwaggerOperation(Summary = "Đăng ký")]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            // Create a new User object
            var user = new User
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            // Attempt to create the user
            var createResult = await _userManager.CreateAsync(user, registerDto.Password);

            if (!createResult.Succeeded)
            {
                return BadRequest(new { Errors = createResult.Errors });
            }

            var roleResult = await _userManager.AddToRoleAsync(user, "User");

            if (!roleResult.Succeeded)
            {
                return StatusCode(500, new { Errors = roleResult.Errors });
            }

            // Retrieve the user's roles
            var roles = await _userManager.GetRolesAsync(user);

            // Return the user data along with a token
            return Ok(new UserAndMoreDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user, roles)
            });
        }
        catch (Exception ex)
        {
            // Log the exception (recommended in production applications)
            // Return a generic error message
            return StatusCode(500, new { Message = "An unexpected error occurred.", Details = ex.Message });
        }
    }



    // Login
    [SwaggerOperation(Summary = "Đăng nhập")]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        /*Tìm user và kiểm tra password*/
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
        if (user == null) return Unauthorized();

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        /*Nếu Login không thành công*/
        if (!result.Succeeded) return Unauthorized("User not found or Password incorrect");

        var roles = await _userManager.GetRolesAsync(user);
        return Ok(new UserAndMoreDto
        {
            UserName = user.UserName!,
            Email = user.Email!,
            Token = _tokenService.CreateToken(user, roles)
        });
    }

    // Logout (POST)
    // Check role, permission, isAuthenticated = token hiện tại (GET)
    [HttpGet]
    [Route("check")]
    public async Task<IActionResult> CheckUserLogin()
    {
        var user = await _userManager.FindByNameAsync("cuong");
        if (user is null) return Unauthorized();
        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenService.CreateToken(user, roles);
        return Ok(roles);
    }

    // Refresh token (POST)

    // Create Avatar
    // [SwaggerOperation(Summary = "Upload avatar")]
    // [HttpPost]
    // [Route("avatar")]
    // public async Task<IActionResult> CreateAvatar(IFormFile? file)
    // {
    //     // if (file == null || file.Length == 0) return BadRequest(new BadRequestResponse("File is empty"));
    //
    //     // const string containerName = "avatar";
    //     // var result = await _blobStorageService.UploadFileAsync(file, containerName);
    //
    //     // return Ok(new { FileUrl = result });
    // }
}