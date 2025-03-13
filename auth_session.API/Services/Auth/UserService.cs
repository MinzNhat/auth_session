using auth_session.Common.Helpers;
using auth_session.Core.Entities.Auth;
using auth_session.Core.Interfaces.Auth;

namespace auth_session.API.Services.Auth
{
    public class UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username) ?? throw new Exception("Account doesn't exist");
            if (user.PasswordHash != HashHelper.HashPassword(password)) throw new Exception("Password does not match");
            return user;
        }

        public async Task<string> RegisterAsync(string username, string password, UserRole role)
        {
            if (await _userRepository.ExistsAsync(username))
                throw new Exception("Username already exists");

            var user = new User
            {
                Username = username,
                PasswordHash = HashHelper.HashPassword(password),
                Role = role,
            };

            await _userRepository.AddAsync(user);
            return "Register successfull";
        }

        public async Task<string> ChangePasswordAsync(string old_password, string new_password)
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                throw new UnauthorizedAccessException();
            }

            string userIdString = _httpContextAccessor.HttpContext.Session.GetString("UserId") ?? throw new Exception("UserId not found in session");
            if (!int.TryParse(userIdString, out int userId))
            {
                throw new Exception("Invalid UserId");
            }

            var user = await _userRepository.GetByIdAsync(userId) ?? throw new Exception("Account doesn't exist");
            if (user.PasswordHash != HashHelper.HashPassword(old_password)) throw new Exception("Old password does not match");
            await _userRepository.ChangePasswordAsync(HashHelper.HashPassword(new_password), user);
            return "Change password successfull";
        }

        public async Task<string> DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id) ?? throw new Exception("Account with id doesn't exist");
            await _userRepository.DeleteAsync(user);
            return "Delete successfull";
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }
    }
}