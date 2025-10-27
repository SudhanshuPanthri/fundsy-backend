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



        #region GET API's

        public async Task<List<ProjectDTO>> GetAllProjectsAsync()
        {
            try
            {
                var data = await _context.Projects.Include(x=>x.User).ToListAsync();
                var response = data.Select(x => new ProjectDTO
                {
                    Title = x.Title,
                    Description = x.Description,
                    ProjectImage = x.Image_Url,
                    Goal = x.Goal_Amount,
                    UserName=x.User.UserName

                }).ToList();

                return response;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                return new List<ProjectDTO>();
            }
        }

        public async Task<List<ProjectDTO>> GetAllProjectsByUserAsync(string email)
        {
            try
            {
                //find the user
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

                //get the user id and get all the projects
                if (user != null)
                {
                    var data = await _context.Projects.Where(x => x.User_id == user.Id).ToListAsync();
                    //convert Project Model to ProjectDTO
                    var response = data.Select(x => new ProjectDTO
                    {
                        Title = x.Title,
                        Description = x.Description,
                        ProjectImage = x.Image_Url,
                        Goal = x.Goal_Amount,
                        UserName = x.User.UserName
                    }).ToList();

                    return response;
                }
                else
                {
                    return new List<ProjectDTO>();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.InnerException);
                return new List<ProjectDTO>();
            }
        }

        #endregion
    }
}
