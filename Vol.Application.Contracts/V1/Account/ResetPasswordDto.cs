using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vol.V1.Account
{
    public class ResetPasswordDto
    {
        public string Email { get; set; }

        [Required]
        public string ResetToken { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
