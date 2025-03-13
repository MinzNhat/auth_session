using auth_session.Core.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using auth_session.Core.Interfaces.Auth;
using auth_session.Infrastructure.Persistence;

namespace auth_session.Infrastructure.Repositories.Auth
{
    public class UserRepository(AuthDbContext context) : IUserRepository
    {
        private readonly AuthDbContext _context = context;

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username);
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task ChangePasswordAsync(string new_password_hash, User user)
        {
            user.PasswordHash = new_password_hash;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}