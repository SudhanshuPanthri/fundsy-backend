using Fundsy_backend.Data;
using Fundsy_backend.DTO;
using Fundsy_backend.Models;
using Fundsy_backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fundsy_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterDTO data)
        {
            var user = await _authService.RegisterAsync(data);

            if (user == null)
            {
                return BadRequest("User with this email already exists.");
            }

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDTO data)
        {
            var token=await _authService.LoginAsync(data);

            if(token == null)
            {
                return BadRequest("Invalid email or password.");
            }

            return Ok(token);
        }
    }
}
