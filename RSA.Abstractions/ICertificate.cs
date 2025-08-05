using Microsoft.IdentityModel.Tokens;

namespace RSA.Abstractions;

public interface ICertificate
{
    RsaSecurityKey Key { get; }
}