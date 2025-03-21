using auth_session.Core.Entities.Auth;

namespace auth_session.Core.Interfaces.Auth
{
    public interface IUserService
    {
        Task<User?> AuthenticateAsync(string username, string password);
        Task<string> RegisterAsync(string username, string password, UserRole role);
        Task<string> ChangePasswordAsync(string old_password, string new_password);
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetUserInfoAsync();
        Task<string> DeleteAsync(int id);
        Task<List<User>> GetAllAsync();
    }
}