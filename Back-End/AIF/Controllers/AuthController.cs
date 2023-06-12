using AIF.Dtos;
using AIF.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AIF.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserServices _userServices;
        private readonly JwtService _jwtService;

        public AuthController(UserServices userServices, JwtService jwtService)
        {
            _userServices = userServices;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            try
            {
                _userServices.CreateUserAsync(dto.Name, dto.Email, dto.Password);

                return Created("success", dto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            try
            {
                var user = _userServices.GetUserByEmail(dto.Email);

                if (user == null)
                    throw new Exception("Invalid User");

                if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                    throw new Exception("Invalid Password");

                var jwt = _jwtService.Generate(user.Id);

                return Ok(new { token = jwt, name = user.Name, email = user.Email });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginDto dto)
        {
            try
            {
                var validation = await ValidateGoogleToken(dto.GoogleToken);
                if (!validation.IsValid)
                    return BadRequest(new { error = validation.ErrorMessage });

                var user = _userServices.GetUserByEmail(dto.Email);
                if (user == null)
                {
                    user = _userServices.CreateUserWithDefaultPassword(dto.Name, dto.Email);
                }

                var jwt = _jwtService.Generate(user.Id);
                return Ok(new { token = jwt, name = dto.Name, email = dto.Email });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("Validate-Google-Token")]
        public async Task<GoogleTokenValidationResult> ValidateGoogleToken(string googleToken)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Failed to validate Google token.", ex);
            }
        }

        [HttpGet("user")]
        public IActionResult GetUser()
        {
            try
            {
                var authorizationHeader = Request.Headers["Authorization"];

                if (string.IsNullOrEmpty(authorizationHeader))
                    return Unauthorized();

                var token = authorizationHeader.ToString();

                var decodedToken = _jwtService.Verify(token);

                int userId = int.Parse(decodedToken.Issuer);

                var user = _userServices.GetUserById(userId);

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
            try
            {
                Response.Cookies.Delete("jwt");

                return Ok(new
                {
                    message = "success"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

    internal class GoogleTokenInfo
    {
        internal object Email;
    }
}
