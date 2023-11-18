using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ProfesiNet.Users.Domain.Entities;
using ProfesiNet.Users.Domain.Interfaces;
using ProfesiNet.Users.Infrastructure.Settings;

namespace ProfesiNet.Users.Infrastructure.Authentication;

internal class JwtProvider(AuthenticationSettings authenticationSettings) : IJwtProvider
{
    public string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, $"{user.Name} {user.Surname}"),
            new(ClaimTypes.Email, user.Email),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(Convert.ToDouble(authenticationSettings.JwtExpiredDays));

        var toke = new JwtSecurityToken(authenticationSettings.JwtIssuer,
            authenticationSettings.JwtIssuer,
            claims,
            expires: expires,
            signingCredentials: cred);

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(toke);
    }
}