using auth_session.Core.Entities.Auth;

namespace auth_session.API.Models.Auth
{
    public class UserDto(int id, string username, UserRole role, DateTime createdAt, bool isActive)
    {
        public int Id { get; } = id;

        public string Username { get; } = username;

        public UserRole Role { get; } = role;

        public DateTime CreatedAt { get; } = createdAt;

        public bool IsActive { get; } = isActive;
    }
}