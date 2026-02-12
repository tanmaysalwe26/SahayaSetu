using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SahayaSetu.DTO.Auth;
using SahayaSetu.DTO.NGO;
using SahayaSetu.Services;

namespace SahayaSetu.Controllers
{
    [AllowAnonymous]
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
        public async Task<IActionResult> RegisterDonor([FromBody] DonorRegisterDto dto)
        {
            try
            {
                var result = await _authService.RegisterDonorAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminRegisterDto dto)
        {
            try
            {
                var result = await _authService.RegisterAdminAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var result = await _authService.LoginUserAsync(dto);
                return Ok(new 
                { 
                    token = result.Token, 
                    userId = result.Id,
                    role = result.Role,
                    name = result.Name,
                    message = result.Message
                });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid credentials");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register-ngo")]
        public async Task<IActionResult> RegisterNgo([FromBody] NgoRegisterDto dto)
        {
            try
            {
                var result = await _authService.RegisterNgoAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login-ngo")]
        public async Task<IActionResult> LoginNgo([FromBody] LoginDto dto)
        {
            try
            {
                var result = await _authService.LoginNgoAsync(dto);
                return Ok(new
                {
                    token = result.Token,
                    role = result.Role,
                    name = result.Name,
                    message = result.Message
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
