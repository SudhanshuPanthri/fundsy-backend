using Fundsy_backend.DTO;
using Fundsy_backend.Models;

namespace Fundsy_backend.Services
{
    public interface IAuthService
    {
        string CreateToken(User user);
        Task<User> RegisterAsync(RegisterDTO data);
        Task<string?> LoginAsync(LoginDTO data);
    }
}
