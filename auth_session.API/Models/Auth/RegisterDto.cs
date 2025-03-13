using auth_session.Core.Entities.Auth;
using System.ComponentModel.DataAnnotations;

namespace auth_session.API.Models.Auth
{
    public class RegisterDto
    {
        public required string Username { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
                ErrorMessage = "Password must be at least 8 characters long, contain an uppercase letter, a lowercase letter, a number and a special character.")]
        public required string Password { get; set; }

        public UserRole Role { get; set; }
    }
}