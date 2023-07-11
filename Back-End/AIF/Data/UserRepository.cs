using AIF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AIF.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly AifDatabaseContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRepository(AifDatabaseContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetLoggedInUser()
        {
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            return await _context.Users.FirstOrDefaultAsync(u => u.Name == userName);
        }

        public async Task<User> UpdateProfilePictureUrlAsync(int userId, string profilePictureUrl)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.ProfilePictureUrl = profilePictureUrl;
                await _context.SaveChangesAsync();
            }
            return user;
        }
    }
}
