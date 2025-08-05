using Jwt.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Jwt;

public class JwtParameters(IConfiguration configuration) : IJwtParameters
{
    public string Issuer { get; } = configuration.
        GetSection("Jwt").
        GetSection("issuer").Value!;
    public string Audience { get; } = configuration.
        GetSection("Jwt").
        GetSection("audience").Value!;
}