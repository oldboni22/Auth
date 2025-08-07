using Microsoft.IdentityModel.Tokens;

namespace RSA.Abstractions;

public interface ICertificate
{
    RsaSecurityKey PrivateKey { get; }
    RsaSecurityKey PublicKey { get; }
}