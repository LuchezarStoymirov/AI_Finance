using System;
using AIF.Models;
using AIF.Data;
using AIF.Dtos;
using AIF.Services;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Newtonsoft.Json;

namespace AIF.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly JwtService _jwtService;

        public AuthController(IUserRepository repository, JwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            return Created("success", _repository.CreateAsync(user));
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _repository.GetByEmail(dto.Email);

            if (user == null) return BadRequest(new { message = "Invalid Credentials" });

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }

            var jwt = _jwtService.Generate(user.Id);

            return Ok(new { token = jwt, name = user.Name, email = user.Email });
        }


        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginDto dto)
        {
            var validation = await ValidateGoogleToken(dto.GoogleToken);
            if (!validation.IsValid)
            {
                return BadRequest(new { error = validation.ErrorMessage });
            }

            var user = _repository.GetByEmail(dto.Email);
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

            var jwt = _jwtService.Generate(user.Id);
            return Ok(new { token = jwt, name = dto.Name, email = dto.Email });

        }

        [HttpPost("Validate-Google-Token")]
        public async Task<GoogleTokenValidationResult> ValidateGoogleToken(string googleToken)
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

        [HttpGet("user")]
        public IActionResult GetUser()
        {
            try
            {
                var authorizationHeader = Request.Headers["Authorization"];

                if (string.IsNullOrEmpty(authorizationHeader))
                {
                    return Unauthorized();
                }

                var token = authorizationHeader.ToString();

                var decodedToken = _jwtService.Verify(token);

                int userId = int.Parse(decodedToken.Issuer);

                var user = _repository.GetById(userId);

                return Ok(user);
            }
            catch (Exception ex)
            {
                return Unauthorized();
            }
        }


        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new
            {
                message = "success"
            });
        }
    }

    internal class GoogleTokenInfo
    {
        internal object Email;
    }
}