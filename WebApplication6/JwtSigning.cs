using Microsoft.IdentityModel.Tokens;

namespace WebApplication6
{

    public interface IJwtSigningEncodingKey
    {
        string SigningAlgorithm { get; }
 
        SecurityKey GetKey();
    }
 

    public interface IJwtSigningDecodingKey
    {
        SecurityKey GetKey();
    }
}