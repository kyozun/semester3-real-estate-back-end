using Microsoft.AspNetCore.Identity;

namespace semester3_real_estate_back_end.Models;

public class User : IdentityUser
{
    public string? Address { get; set; }
    public string? AvatarUrl { get; set; }


    // Navigation Property
}