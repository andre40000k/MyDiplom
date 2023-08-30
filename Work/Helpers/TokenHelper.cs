using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace LoginComponent.Helpers
{
    public class TokenHelper
    {
        public const string _issuer = "https://test.com";
        public const string _audience = "https://test.com";
        public const string _secret = "vPas5BpRw29JIiNSJKW3O5YMtXKnDQSQpvUKNa3jYMPVsgMLsNTYI6zWmTKegrFFsdeIDhYH6wIwtxSlhmay1QSlkjlEYF7F0FBY";

        //public readonly IConfiguration _config;
        //public TokenHelper(IConfiguration configuration)
        //{
        //    _issuer = configuration.GetValue<string>("Auth:Issuer")!;
        //    _audience = configuration.GetValue<string>("Auth:Audience")!;
        //    _secret = configuration.GetValue<string>("Auth:Secret")!;
        //}

        public static async Task<string> GenerateAccessToken(Guid userId)
        {
            var mySecurityKey = new SymmetricSecurityKey(Convert.FromBase64String(_secret));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString())
                }),
                Expires = DateTime.Now.AddMinutes(15),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return await Task.Run(() => tokenHandler.WriteToken(securityToken));
        }

        public static async Task<string> GenerateRefreshToken()
        {
            var secureRandomBytes = new byte[32];

            using var randomNumberGenerator = RandomNumberGenerator.Create();
            await Task.Run(() => randomNumberGenerator.GetBytes(secureRandomBytes));

            var refreshToken = Convert.ToBase64String(secureRandomBytes);
            return refreshToken;
        }
    }
}
