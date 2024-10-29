using Domain.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace InfraData.Authentication
{
    public class DecodeToken : IDecodeToken
    {
        private const string SecretKey = "dhfjkasfdsafdsfsahdfjkahsdjkfahsdjfhajksdfhakjfhksdfhadksjhfadkjfalsdfjkfjdfkal";
        private readonly SymmetricSecurityKey _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

        public int? Decode(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _key
            };
            try
            {
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;

                var userId = jwtToken.Claims.First(x => x.Type == "Id").Value;

                return int.Parse(userId);
            }
            catch
            {
                return null;
            }
        }
    }
}
