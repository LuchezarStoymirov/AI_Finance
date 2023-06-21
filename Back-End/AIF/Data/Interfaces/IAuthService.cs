using AIF.Controllers;
using AIF.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AIF.Models.GoogleModels;

namespace AIF.Services
{
    public interface IAuthService
    {
        Task<IActionResult> RegisterAsync(RegisterDto dto);

        Task<IActionResult> LoginAsync(LoginDto dto);

        Task<IActionResult> GoogleLoginAsync(GoogleLoginDto dto);

        Task<GoogleTokenValidationResult> ValidateGoogleTokenAsync(string googleToken);

        Task<IActionResult> GetUserAsync();

        Task<IActionResult> LogoutAsync();
    }
}
