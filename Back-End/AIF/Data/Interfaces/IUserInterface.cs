﻿using AIF.Models;
using System.Threading.Tasks;

namespace AIF.Data
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByIdAsync(int id);
        Task<User> UpdateAsync(User user);
        Task<User> GetLoggedInUser();
    }
}
