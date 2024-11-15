using Microsoft.AspNetCore.Identity;

namespace semester3_real_estate_back_end.Models;

public class Role : IdentityRole
{
    public string? Description { get; set; }

}