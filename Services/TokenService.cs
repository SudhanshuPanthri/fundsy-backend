using Fundsy_backend.DTO;
using Fundsy_backend.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Fundsy_backend.Services
{
    public class TokenService
    {
        private readonly IConfiguration? _configuration;

        public TokenService(IConfiguration? configuration)
        {
            _configuration = configuration;
        }
        public string CreateToken(LoginDTO user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!));
            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor=new JwtSecurityToken(
                issuer:_configuration.GetValue<string>("AppSettings:Issuer"),
                audience:_configuration.GetValue<string>("AppSettings:Audience"),
                claims:claims,
                expires:DateTime.Now.AddDays(1),
                signingCredentials:creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
