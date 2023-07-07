using AIF.Models;
using AIF.Services;
using System.Threading.Tasks;

namespace AIF.Data
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task CreateAsync(Services.UserService user);
        Task<User> GetByEmailAsync(string email);

        Task<User> GetByIdAsync(int id);
    }
}
