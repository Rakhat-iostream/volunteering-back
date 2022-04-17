using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vol.V1.Account
{
    public class UpdateUserProfileDto
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? PhoneNumber { get; set; }

        [MaxLength(6)]
        public string? PreferLanguage { get; set; }
    }
}
