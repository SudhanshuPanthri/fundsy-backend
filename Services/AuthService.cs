using Fundsy_backend.Data;
using Fundsy_backend.DTO;
using Fundsy_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Fundsy_backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDBContext _context;

        public AuthService(IConfiguration configuration, AppDBContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<User?> RegisterAsync(RegisterDTO data)
        {
            User user = new User();
            if (await _context.Users.FirstOrDefaultAsync(x => x.Email == data.Email) != null)
            {
                return null;
            }

            var hashedPassword = new PasswordHasher<User>().HashPassword(user, data.Password);
            user.UserName = data.UserName;
            user.Email = data.Email;
            user.PasswordHash = hashedPassword;
            user.Role_id = data.Role_id;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<TokenResponseDTO?> LoginAsync(LoginDTO data)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == data.Email);

            if (user == null)
            {
                return null;
            }
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, data.Password) == PasswordVerificationResult.Failed)
            {
                return null;
            }

            var response = new TokenResponseDTO
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshToken(user)
            };

            return response;
        }

        public async Task<TokenResponseDTO?> RefreshTokensAsync(RefreshTokenRequestDTO data)
        {
            var user = await ValidRefreshTokenAsync(data.Id, data.RefreshToken);
            if (user == null)
            {
                return null;
            }

            var response = new TokenResponseDTO
            {
                AccessToken = CreateToken(user),
                RefreshToken = await GenerateAndSaveRefreshToken(user)
            };

            return response;

        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<string> GenerateAndSaveRefreshToken(User user)
        {
            var refreshToken = GenerateRefreshToken();
            user.Refresh_Token = refreshToken;
            user.Refresh_Token_Expiry_Time = DateTime.Now.AddDays(7);
            await _context.SaveChangesAsync();
            return refreshToken;
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("AppSettings:Issuer"),
                audience: _configuration.GetValue<string>("AppSettings:Audience"),
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }

        private async Task<User?> ValidRefreshTokenAsync(Guid id, string refreshToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null || user.Refresh_Token != refreshToken || user.Refresh_Token_Expiry_Time <= DateTime.UtcNow)
            {
                return null;
            }
            return user;
        }
    }
}
