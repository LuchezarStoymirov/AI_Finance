using System;
using AIF.Models;

namespace AIF.Services
{
	public interface IUserService
	{
        Task<User> CreateUserAsync(string name, string email, string password);
        Task<User> GetUserByEmail(string email);
        Task<User> UserCreation(string name, string email);
        User GetUserById(int userId);
    }
}

