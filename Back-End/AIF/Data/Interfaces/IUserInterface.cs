using AIF.Models;

namespace AIF.Data
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        User GetByEmail(string email);
        User GetById(int id);
    }
}