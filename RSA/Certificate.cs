using Microsoft.IdentityModel.Tokens;
using RSA.Abstractions;

namespace RSA;

public class Certificate : ICertificate
{
    public RsaSecurityKey Key { get; }
    
    public Certificate(string path)
    {
        var rsa = System.Security.Cryptography.RSA.Create();
        
        var privatePath = path + ".private.pem";
        var publicPath = path + ".public.pem";

        if (File.Exists(privatePath) && File.Exists(publicPath))
        {
            rsa.ImportFromPem(privatePath);
            rsa.ImportFromPem(publicPath);

            Key = new RsaSecurityKey(rsa);
            return;
        }
        
        File.WriteAllText(publicPath,rsa.ExportRSAPublicKeyPem());
        File.WriteAllText(privatePath,rsa.ExportRSAPrivateKeyPem());

        Key = new RsaSecurityKey(rsa);
    }
    
}