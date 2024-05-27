using Microsoft.IdentityModel.Tokens;
using QuizzApp.Interfaces;
using QuizzApp.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QuizzApp.Token
{
    public class TokenService : ITokenService
    {
        private readonly string _SecretKey;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration)
        {
            _SecretKey = configuration.GetSection("TokenKey").GetSection("JWT").Value.ToString();
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_SecretKey));
        }

        public string GenerateToken(User user)
        {
            string token = string.Empty;
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserEmail),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userid", user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var credential = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
            var myToken = new JwtSecurityToken(null, null, claims, expires: DateTime.Now.AddDays(1), signingCredentials: credential);
            return new JwtSecurityTokenHandler().WriteToken(myToken).ToString();

        }
    }
}
