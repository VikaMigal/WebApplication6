using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebApplication6
{
    
    public class SigningSymmetricKey : IJwtSigningEncodingKey, IJwtSigningDecodingKey
    {
        private readonly SymmetricSecurityKey _secretKey;
 
        public string SigningAlgorithm { get; } = SecurityAlgorithms.HmacSha256;
 
        public SigningSymmetricKey(string key)
        {
            this._secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }
 
        public SecurityKey GetKey() => this._secretKey;
    }
}