using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Jwt.Abstractions;
using Microsoft.IdentityModel.Tokens;
using RSA.Abstractions;

namespace Jwt;

public class JwtManager(ICertificate certificate, JwtParameters parameters) : IJwtManager
{
    private readonly ICertificate _certificate = certificate;

    private readonly string _issuer = parameters.Issuer;
    private readonly string _audience = parameters.Audience;
    
    public string CreateToken(int userId)
    {
        var claims = new List<Claim>
        {
            new Claim("user_id", $"{userId}")
        };

        var credentials = new SigningCredentials(_certificate.Key, SecurityAlgorithms.RsaSha256);
        
        var tokenDescription = new JwtSecurityToken
        (
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(tokenDescription);
    }
}