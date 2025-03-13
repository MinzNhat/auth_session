using auth_session.Core.Entities.Auth;

namespace auth_session.Core.Interfaces.Auth
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUsernameAsync(string username);
        Task ChangePasswordAsync(string new_password, User user);
        Task AddAsync(User user);
        Task<bool> ExistsAsync(string username);
        Task DeleteAsync(User user);
    }
}