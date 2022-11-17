using BussinessLayer.Service.Interface;
using DataAccessLayer.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BussinessLayer.Service.Implement
{
    public class TokenGenerateService : ITokenGenerateService
    {
        private readonly IConfiguration _configuration;

        public TokenGenerateService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(UserEntity user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("userId",user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("securityStamp",user.SecurityStamp)
            };
            var claimIdentity = new ClaimsIdentity(claims);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claimIdentity.Claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: signIn);

            string strToken = new JwtSecurityTokenHandler().WriteToken(token);
            return strToken;
        }
    }
}