using Fundsy_backend.DTO;
using Fundsy_backend.Models;
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

        [Authorize]
        [HttpGet("all")]
        public async Task<ActionResult<List<Project>>> GetAllProjectsAsync()
        {
            try
            {
                var data = await _projectService.GetAllProjectsAsync();
                return Ok(data);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<ActionResult<List<Project>>> GetAllProjectsByUser([FromQuery] string email)
        {
            try
            {
                var data=await _projectService.GetAllProjectsByUserAsync(email);
                return Ok(data);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
