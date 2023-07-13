using AIF.Dtos;
using AIF.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AIF.Models.GoogleModels;

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

        [HttpPost("update-user-info")]
        public async Task<IActionResult> UpdateUserInfo(UpdateUserInfoDto dto)
        {
            try
            {
                return await _authService.UpdateUserInfoAsync(dto);
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
