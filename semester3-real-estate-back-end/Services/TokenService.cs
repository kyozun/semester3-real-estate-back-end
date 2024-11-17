using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using semester3_real_estate_back_end.Interfaces;
using semester3_real_estate_back_end.Models;
using semester4.Interfaces;

namespace semester3_real_estate_back_end.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly SymmetricSecurityKey _securityKey;

    /*Lấy data từ appsettings.json*/
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
        _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]!));
    }

    public string CreateToken(User user, IList<string> roles)
    {
        var claims = GetClaims(user, roles);

        var credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = credentials,
            Issuer = _configuration["JWT:Issuer"],
            Audience = _configuration["JWT:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        /*Trả về Token dạng string*/
        return tokenHandler.WriteToken(token);
    }

    private static List<Claim> GetClaims(User user, IList<string> roles)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.GivenName, user.UserName!)
        };

        // Thêm role vào claim
        foreach (var role in roles) claims.Add(new Claim(ClaimTypes.Role, role));

        return claims;
    }
}