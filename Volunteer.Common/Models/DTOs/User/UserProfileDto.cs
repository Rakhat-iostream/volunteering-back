using System;
using Volunteer.Common.Models.Domain.Enum;

namespace Volunteer.Common.Models.DTOs.User
{
    public class UserProfileDto
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string? Email { get; set; }
        public string Phone { get; set; }
        public UserRoles Role { get; set; }
        public UserStatus Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public string Avatar { get; set; }
    }
}
