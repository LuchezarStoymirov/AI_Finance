using System.Linq;
using AIF.Data;
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
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create user.", ex);
            }
        }

        public User GetByEmail(string email)
        {
            try
            {
                return _context.Users.FirstOrDefault(u => u.Email == email);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve user by email.", ex);
            }
        }

        public User GetById(int id)
        {
            try
            {
                return _context.Users.FirstOrDefault(u => u.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve user by ID.", ex);
            }
        }
    }
}
