using AIF.Data;
using AIF.Models;
using System;
using System.Threading.Tasks;

namespace AIF.Services
{
    public class UserServices
    {
        private readonly IUserRepository _repository;

        public UserServices(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> CreateUserAsync(string name, string email, string password)
        {
            var user = new User
            {
                Name = name,
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(password)
            };

            await _repository.CreateAsync(user);

            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _repository.GetByEmailAsync(email);
        }

        public async Task<User> CreateUserWithDefaultPassword(string name, string email)
        {
            var user = new User
            {
                Email = email,
                Name = name,
                Password = "default google login password"
            };

            await _repository.CreateAsync(user);

            return user;
        }

        public User GetUserById(int userId)
        {
            return _repository.GetById(userId);
        }
    }
}
