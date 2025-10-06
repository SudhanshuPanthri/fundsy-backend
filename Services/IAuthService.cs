using Fundsy_backend.DTO;
using Fundsy_backend.Models;

namespace Fundsy_backend.Services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(RegisterDTO data);
        Task<TokenResponseDTO?> LoginAsync(LoginDTO data);
        Task<TokenResponseDTO?> RefreshTokensAsync(RefreshTokenRequestDTO data);
    }
}
