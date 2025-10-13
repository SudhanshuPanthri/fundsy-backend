using Fundsy_backend.Data;
using Fundsy_backend.DTO;
using Fundsy_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Fundsy_backend.Services
{
    public class ProjectService : IProjectService
    {
        private readonly AppDBContext _context;

        public ProjectService(AppDBContext context)
        {
            _context= context;  
        }

        public async Task<bool> CreateProjectAsync(ProjectDTO data)
        {
            try
            {
                //get the user ID from the email

                var user=await _context.Users.FirstOrDefaultAsync(x=>x.Email==data.UserEmail);

                if(user==null)
                {
                    return false;
                }

                else
                {
                    Project project = new Project();

                    project.Title = data.Title!;
                    project.Description = data.Description!;
                    project.Goal_Amount = data.Goal;
                    project.Category_id = data.CategoryId;
                    project.Image_Url = data.ProjectImage;
                    project.User_id = user.Id;

                    await _context.Projects.AddAsync(project);
                    await _context.SaveChangesAsync();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
