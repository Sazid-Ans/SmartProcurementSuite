using IdentityService.DomainService;
using IdentityService.Models;
using IdentityService.Models.Dtos;
using IdentityService.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IAuthService _authService;
        
        private readonly IConfiguration _config;
      


        public AuthController(IAuthService authService, IConfiguration config)
        {
            _authService = authService;
            _config = config;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid request");

            var result = await _authService.RegisterUserAsync(dto);
            if (!result.Succeeded)
                return BadRequest(result.ErrorMessage);

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto user)
        {
            if (user == null)
                return BadRequest("User data is required.");

            // Here you would typically add logic to authenticate the user
            // and return a JWT token if successful.
            var result = await _authService.LoginUserAsync(user);

            if (!result.Succeeded) return Unauthorized(result.ErrorMessage);

            return Ok(new
            {
                Token = result.Jwt,
                ExpiresIn = result.ExpiresInMinutes
            });
        }
    }
}
