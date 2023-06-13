using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using AIF.Models;

namespace AIF.Services.Implementation
{
    public interface IRepository<T>
    {
        // ... interface definition ...
    }
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UserService> _logger;

        public UserService(IRepository<User> repository, UserManager<User> userManager, ILogger<UserService> logger)
        {
            _repository = repository;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<BaseResponseViewModel> RegisterAsync(UserRegistrationViewModel viewUser)
        {
            var messages = "";

            var user = new User
            {
                UserName = viewUser.Email,
                Email = viewUser.Email,
                FirstName = viewUser.FirstName,
                LastName = viewUser.LastName
            };

            try
            {
                var result = await _userManager.CreateAsync(user, viewUser.Password);

                if (result.Succeeded)
                {
                    return new BaseResponseViewModel { Success = true, ErrorMessage = "" };
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        messages += $"{error.Description}%";
                    }

                    return new BaseResponseViewModel { Success = false, ErrorMessage = messages.Remove(messages.Length - 1) };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                messages += $"{ex.Message}";
                return new BaseResponseViewModel { Success = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<TokensResponseViewModel> LoginAsync(UserLoginViewModel user)
        {
            var messages = "";

            try
            {
                var result = await _userManager.CheckPasswordAsync(user.Email, user.Password);

                if (result)
                {
                    var dbUser = _repository.FindByCondition(u => u.Email == user.Email).FirstOrDefault();

                    if (dbUser != null)
                    {
                        return new TokensResponseViewModel { Success = true, ErrorMessage = "", Tokens = GenerateAccessToken(dbUser.Email, dbUser.Id) };
                    }
                }

                return new TokensResponseViewModel { Success = false, ErrorMessage = "Invalid email or password", Tokens = null };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred");
                messages += $"{ex.Message}";
                return new TokensResponseViewModel { Success = false, ErrorMessage = messages, Tokens = null };
            }
        }

        private string GenerateAccessToken(string email, int userId)
        {
            // Implement your access token generation logic here
            // Generate and return the access token
            string accessToken = $"{email}_{userId}";
            return accessToken;
        }
    }

    public class UserRegistrationViewModel
    {
        public string Password { get; internal set; }
        public object Email { get; internal set; }
        public object FirstName { get; internal set; }
    }

    public class UserLoginViewModel
    {
        public User Email { get; internal set; }
        public string Password { get; internal set; }
    }

    internal class TokensResponseViewModel
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string Tokens { get; set; }
    }
    internal class BaseResponseViewModel
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
    public interface IUserService
    {
    }
}