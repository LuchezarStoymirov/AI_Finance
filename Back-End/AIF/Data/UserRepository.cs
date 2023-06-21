using AIF.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }
    }
}
