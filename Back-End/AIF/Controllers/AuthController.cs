using AIF.Dtos;
using AIF.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AIF.Models.GoogleModels;
using AIF.Data;
using Microsoft.EntityFrameworkCore;
using AIF.Models;


namespace AIF.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            try
            {
                return await _authService.RegisterAsync(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                return await _authService.LoginAsync(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("updateuser")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto dto)
        {
            using (var context = new AifDatabaseContext())
            {
                var user = await context.Users.FirstOrDefaultAsync(u =>
                    u.Name == dto.OldUsername || u.Email == dto.OldEmail);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                if (!string.IsNullOrEmpty(dto.NewUsername))
                {
                    user.Name = dto.NewUsername;
                }

                if (!string.IsNullOrEmpty(dto.NewEmail))
                {
                    user.Email = dto.NewEmail;
                }

                await context.SaveChangesAsync();

                return Ok("User updated successfully");
            }
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginDto dto)
        {
            try
            {
                return await _authService.GoogleLoginAsync(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("Validate-Google-Token")]
        public async Task<GoogleTokenValidationResult> ValidateGoogleToken(string googleToken)
        {
            try
            {
                return await _authService.ValidateGoogleTokenAsync(googleToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                return await _authService.GetUserAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                return await _authService.LogoutAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
