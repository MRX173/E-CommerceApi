using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Options;
using Domain.UserAggregate.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class JwtService
{
    private readonly JwtSettings _jwtSettings;
    private readonly byte[] _key; 

    public JwtService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
        _key = Encoding.ASCII.GetBytes(_jwtSettings.SigningKey);
    }

    public JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();

    public string GenerateToken(ClaimsIdentity identity)
    {
        SecurityTokenDescriptor tokenDescriptor = TokenDescriptor(identity);
        SecurityToken? securityToken = TokenHandler.CreateToken(tokenDescriptor);
        string? accessToken = TokenHandler.WriteToken(securityToken);
        return accessToken;
    }

    private SecurityTokenDescriptor TokenDescriptor(ClaimsIdentity identity)
    {
        return new SecurityTokenDescriptor
        {
            Subject = identity,
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key)
                , SecurityAlgorithms.HmacSha256)
        };
    }
}