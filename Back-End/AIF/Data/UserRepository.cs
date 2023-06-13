using System.Linq;
using System.Threading.Tasks;
using AIF.Models;

namespace AIF.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly AifDatabaseContext _context;

        public UserRepository(AifDatabaseContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await Task.FromResult(_context.Users.FirstOrDefault(u => u.Email == email));
        }

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
