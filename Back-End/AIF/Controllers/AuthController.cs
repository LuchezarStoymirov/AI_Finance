using System;
using AIF.Models;
using AIF.Data;
using AIF.Dtos;
using AIF.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using System.Threading.Tasks;

namespace AIF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserRepository _repository;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly JwtService _jwtService;

        public AuthController(IUserRepository repository, SignInManager<User> signInManager, UserManager<User> userManager, JwtService jwtService)
        {
            _repository = repository;
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new { message = "Failed to register the user!" });
            }
            else
            {
                return Created("Register succesfull!", user);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
                var user = await _userManager.FindByEmailAsync(dto.Email);
                if (User == null)
                {
                    return BadRequest(new { message = "Wrong credentials!" });
                }

                var jwt = _jwtService.Generate(user.Id);

                Response.Cookies.Append("jwt", jwt, new CookieOptions { HttpOnly = true });

                return Ok(new { message = "Login succesfull!" });
        }
    }
}