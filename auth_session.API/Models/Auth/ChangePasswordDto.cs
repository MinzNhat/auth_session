using System.ComponentModel.DataAnnotations;

namespace auth_session.API.Models.Auth
{
    public class ChangePasswordDto
    {
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
                        ErrorMessage = "Old password must be at least 8 characters long, contain an uppercase letter, a lowercase letter, a number and a special character.")]
        public required string OldPassword { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
                        ErrorMessage = "New Password must be at least 8 characters long, contain an uppercase letter, a lowercase letter, a number and a special character.")]
        public required string NewPassword { get; set; }
    }
}