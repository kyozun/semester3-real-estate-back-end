﻿using System.ComponentModel.DataAnnotations;

namespace semester3_real_estate_back_end.DTO.User;

public class RegisterDto
{
    [Required] public required string Username { get; set; }

    [Required] [EmailAddress] public required string Email { get; set; }


    [Required] public required string Password { get; set; }
}