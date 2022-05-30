using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer.Common.Models.Domain.Enum
{
    public enum ValidationStatus
    {
        [Display(Name = "-")]
        None,

        Verified = 1,

        Unverified = 2,
    }
}
