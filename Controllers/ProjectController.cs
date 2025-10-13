using Fundsy_backend.DTO;
using Fundsy_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fundsy_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [Authorize]
        [HttpPost("create")]

        public async Task<ActionResult> CreateProjectAsync([FromBody] ProjectDTO data)
        {
            try
            {
                var response=await _projectService.CreateProjectAsync(data);
                if(response)
                {
                    return Ok("Project created successfully.");
                }
                else
                {
                    return BadRequest("Failed to create project.");
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
