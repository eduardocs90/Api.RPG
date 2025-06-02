using Domain.Authentication;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InfraData.Authentication
{
    public class TokenGenerator : ITokenGenerator
    {
        public dynamic Generator(User user)
        {
            var claims = new List<Claim> {
            new Claim("Email", user.Email),
            new Claim("Name", user.Name),
            new Claim("role", user.Role.ToString()),
            new Claim("Id", user.Id.ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("dhfjkasfdsafdsfsahdfjkahsdjkfahsdjfhajksdfhakjfhksdfhadksjhfadkjfalsdfjkfjdfkal"));
            var tokenData = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),          
            claims: claims
                );
            var token = new JwtSecurityTokenHandler().WriteToken(tokenData);
            return new
            {
                acess_token = token,
            };
        }
    }
}
