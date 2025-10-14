using Fundsy_backend.DTO;

namespace Fundsy_backend.Services
{
    public interface IProjectService
    {
        Task<bool> CreateProjectAsync(ProjectDTO data);
    }
}
