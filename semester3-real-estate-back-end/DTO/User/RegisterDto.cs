﻿using System.ComponentModel.DataAnnotations;

namespace semester4.DTO.User;

public class RegisterDto
{
    [Required] public required string Username { get; set; }

    [Required] [EmailAddress] public required string Email { get; set; }


    [Required] public required string Password { get; set; }
}