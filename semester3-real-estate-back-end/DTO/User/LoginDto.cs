using System.ComponentModel.DataAnnotations;

namespace semester3_real_estate_back_end.DTO.User;

public class LoginDto
{
    [Required] public required string Username { get; set; }

    [Required] public required string Password { get; set; }
}