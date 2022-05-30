using System.ComponentModel.DataAnnotations;

namespace Volunteer.Common.Models.DTOs.Auth
{
    public class VerifyCodeDto
    {
        [Phone]
        public string Phone { get; set; }
        public string Code { get; set; }
    }
}
