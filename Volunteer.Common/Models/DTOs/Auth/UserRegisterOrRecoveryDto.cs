using System.ComponentModel.DataAnnotations;

namespace Volunteer.Common.Models.DTOs.Auth
{
    public class UserRegisterOrRecoveryDto
    {
        [Phone]
        public string Phone { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }
}
