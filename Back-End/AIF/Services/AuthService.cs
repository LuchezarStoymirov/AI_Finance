using AIF.Controllers;
using AIF.Data;
using AIF.Dtos;
using AIF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using AIF.Models.GoogleModels;

namespace AIF.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;
        private readonly IJwtService _jwtService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IUserRepository repository, IJwtService jwtService, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _jwtService = jwtService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> RegisterAsync(RegisterDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                ProfilePictureUrl = "s3://ai-finances/Profile Pictures/Default Profile Picture.png"
            };

            await _repository.CreateAsync(user);

            return new CreatedResult("success", user);
        }

        public async Task<IActionResult> LoginAsync(LoginDto dto)
        {
            var user = await _repository.GetByEmailAsync(dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                return new BadRequestObjectResult(new { message = "Invalid Credentials" });

            var jwt = await _jwtService.GenerateAsync(user.Id);

            return new OkObjectResult(new { token = jwt, name = user.Name, email = user.Email });
        }

        public async Task<IActionResult> GoogleLoginAsync(GoogleLoginDto dto)
        {
            var validation = await ValidateGoogleTokenAsync(dto.GoogleToken);
            if (!validation.IsValid)
            {
                return new BadRequestObjectResult(new { error = validation.ErrorMessage });
            }

            var user = await _repository.GetByEmailAsync(dto.Email);
            if (user == null)
            {
                user = new User
                {
                    Email = dto.Email,
                    Name = dto.Name,
                    Password = "default google login password"
                };
                await _repository.CreateAsync(user);
            }

            var jwt = await _jwtService.GenerateAsync(user.Id);

            return new OkObjectResult(new { token = jwt, name = dto.Name, email = dto.Email });
        }

        public async Task<GoogleTokenValidationResult> ValidateGoogleTokenAsync(string googleToken)
        {
            using (var httpClient = new HttpClient())
            {
                var validationEndpoint = "https://oauth2.googleapis.com/tokeninfo?id_token=" + googleToken;
                var response = await httpClient.GetAsync(validationEndpoint);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var tokenInfo = JsonConvert.DeserializeObject<GoogleTokenInfo>(responseContent);

                    var validationResult = new GoogleTokenValidationResult
                    {
                        IsValid = true,
                        Email = tokenInfo.Email
                    };

                    return validationResult;
                }
                else
                {
                    var validationResult = new GoogleTokenValidationResult
                    {
                        IsValid = false,
                        ErrorMessage = "Token validation failed."
                    };

                    return validationResult;
                }
            }
        }

        public async Task<IActionResult> GetUserAsync()
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return new UnauthorizedResult();
            }

            var token = authorizationHeader.ToString();

            var decodedToken = await _jwtService.VerifyAsync(token);

            int userId = int.Parse(decodedToken.Issuer);

            var user = await _repository.GetByIdAsync(userId);

            return new OkObjectResult(user);
        }

        public async Task<bool> ValidateTokenAsync(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            var decodedToken = await _jwtService.VerifyAsync(token);

            if (decodedToken == null)
            {
                return false;
            }

            int userId = int.Parse(decodedToken.Issuer);

            var user = await _repository.GetByIdAsync(userId);

            return user != null;
        }

        public async Task<IActionResult> LogoutAsync()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("jwt");

            return new OkObjectResult(new
            {
                message = "success"
            });
        }
    }
}
