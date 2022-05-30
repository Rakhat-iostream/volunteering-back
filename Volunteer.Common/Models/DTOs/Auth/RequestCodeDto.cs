using System.ComponentModel.DataAnnotations;

namespace Volunteer.Common.Models.DTOs.Auth
{
    public class RequestCodeDto
    {
        [Phone]
        public string Phone { get; set; }
    }
}
