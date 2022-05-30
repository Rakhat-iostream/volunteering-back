using System.ComponentModel.DataAnnotations;
using Volunteer.Common.Models.Domain.Enum;

namespace Volunteer.Common.Models.DTOs.Auth
{
    public class UserRegisterOrRecoveryDto
    {
        public string Login { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Avatar { get; set; }
        public string? Email { get; set; }
        public UserRoles? Role { get; set; }
        [Phone]
        public string Phone { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }
}
