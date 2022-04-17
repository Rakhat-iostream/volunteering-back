using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vol.V1.Account
{
    public class ChangePasswordInput
    {
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
