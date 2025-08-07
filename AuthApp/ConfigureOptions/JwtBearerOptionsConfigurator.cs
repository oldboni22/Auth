using System.Diagnostics;
using Jwt.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RSA.Abstractions;

namespace AuthApp.ConfigureOptions;

public class JwtBearerOptionsConfigurator(ICertificate certificate, IJwtParameters parameters) : 
    IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly ICertificate _certificate = certificate;
    private readonly IJwtParameters _parameters = parameters;

    public void Configure(JwtBearerOptions options)
    {
        Configure(JwtBearerDefaults.AuthenticationScheme, options);
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        if(name != JwtBearerDefaults.AuthenticationScheme)
            return;
        
        
        options.TokenValidationParameters = new TokenValidationParameters 
        {
            ValidateLifetime = true,
                    
            ValidateIssuer = true,
            ValidIssuer = _parameters.Issuer,
            ValidateAudience = true,
            ValidAudience = _parameters.Audience,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _certificate.PublicKey,
        };
        
    }
}