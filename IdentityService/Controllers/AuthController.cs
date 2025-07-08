using IdentityService.Models;
using IdentityService.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult Register([FromBody]RegistrationRequestDto RegisterUserRequest)
        {
            if (RegisterUserRequest == null)
            {
                return BadRequest("User data is required.");
            }
            // Here you would typically add logic to save the user to the database
            // and handle any errors that may occur.
            return Ok("User registered successfully.");
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody]ApplicationUser user)
        {
            if (user == null)
            {
                return BadRequest("User data is required.");
            }
            // Here you would typically add logic to authenticate the user
            // and return a JWT token if successful.
            return Ok("User logged in successfully.");
        }
    }
}
