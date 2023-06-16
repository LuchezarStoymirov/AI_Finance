using AIF.Models;
using System.Threading.Tasks;

namespace AIF.Data
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<User> GetByEmailAsync(string email);
        //User GetByEmailAsync(string email);
        User GetByIdAsync(int id);
    }
}
