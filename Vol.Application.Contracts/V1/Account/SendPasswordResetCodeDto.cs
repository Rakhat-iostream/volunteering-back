using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vol.V1.Account
{
    public class SendPasswordResetCodeDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string ReturnUrl { get; set; }

        public string ReturnUrlHash { get; set; }
    }
}
