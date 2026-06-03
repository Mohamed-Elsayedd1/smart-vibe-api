using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SmartBayt.Models;

namespace SmartBayt.Helpers;

public class JwtHelper(IConfiguration config)
{
    public string GenerateToken(AppUser user)
    {
        // الـ secret بييجي من Environment Variable أولاً
        var secret = Environment.GetEnvironmentVariable("JWT_SECRET")
            ?? config["Jwt:Secret"]
            ?? throw new InvalidOperationException("JWT Secret غير موجود.");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email,           user.Email),
            new Claim(ClaimTypes.Role,            user.Role),
            new Claim("fullName",                 user.FullName ?? ""),
        };

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
