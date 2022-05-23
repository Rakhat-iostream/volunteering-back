using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer.Common.Models.Domain.Enum
{
    public enum MembershipStatus
    {
        [Display(Name = "-")]
        None,

        NeedOrganizationApprove = 1,

        OrganizationAccepted = 2,

        OrganizationRejected = 3,

        Kicked = 4
    }
}
