namespace Jwt.Abstractions;

public interface IJwtParameters
{
    string Issuer { get; }
    string Audience { get; }
}