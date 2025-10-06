using Fundsy_backend.Models;

namespace Fundsy_backend.Services
{
    public interface ILookupService
    {
        Task<List<Role>> GetRolesAsync();
        Task<List<Status>> GetStatusesAsync();
    }
}
