using Volunteer.Common.Models.Domain.Enum;

namespace Volunteer.Common.Models.DTOs.User
{
    public class UserAddActionDto
    {
        public string Login { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string RepeatedPassword { get; set; }
        public UserRoles Role { get; set; }
        public UserStatus? Status { get; set; }
        public string Position { get; set; }
    }
}
