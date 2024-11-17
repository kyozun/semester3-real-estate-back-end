using System.ComponentModel.DataAnnotations;

namespace semester4.DTO.User;

public class LoginDto
{
    [Required] public required string Username { get; set; }

    [Required] public required string Password { get; set; }
}