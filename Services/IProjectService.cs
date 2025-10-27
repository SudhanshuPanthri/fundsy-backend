using Fundsy_backend.DTO;

namespace Fundsy_backend.Services
{
    public interface IProjectService
    {
        Task<bool> CreateProjectAsync(ProjectDTO data);

        //All get API's
        Task<List<ProjectDTO>> GetAllProjectsAsync();
        Task<List<ProjectDTO>> GetAllProjectsByUserAsync(string email);
    }
}
