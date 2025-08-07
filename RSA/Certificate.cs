using Microsoft.IdentityModel.Tokens;
using RSA.Abstractions;

namespace RSA;

public class Certificate : ICertificate
{
    public RsaSecurityKey PrivateKey { get; }
    public RsaSecurityKey PublicKey { get; private set; }

    public Certificate(string path)
    {
        var privateRsa = System.Security.Cryptography.RSA.Create();
        
        var privatePath = path + ".private.pem";
        var publicPath = path + ".public.pem";

        if (File.Exists(privatePath))
        {
            privateRsa.ImportFromPem(File.ReadAllText(privatePath));
        }
        else
        {
            File.WriteAllText(privatePath,privateRsa.ExportRSAPrivateKeyPem());    
            File.WriteAllText(publicPath,privateRsa.ExportRSAPublicKeyPem());    
        }
        
        InitializePublicKey(privateRsa);
        PrivateKey = new RsaSecurityKey(privateRsa);
        
    }

    private void InitializePublicKey(System.Security.Cryptography.RSA privateRsa)
    {
        var publicKeyParameters = privateRsa.ExportParameters(false);
        var publicRsa = System.Security.Cryptography.RSA.Create();
        
        publicRsa.ImportParameters(publicKeyParameters);
        PublicKey = new RsaSecurityKey(publicRsa);
    }
    
}