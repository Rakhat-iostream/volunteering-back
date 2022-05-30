using System.ComponentModel.DataAnnotations;

namespace Volunteer.Common.Models.Domain.Enum
{
    public enum MembershipStatus
    {
        [Display(Name = "-")]
        None,

        NeedOrganizationApprove = 1,

        OrganizationAccepted = 2,

        OrganizationRejected = 3,

        Kicked = 4,

        NeedVolunteerApprove = 5,

        VolunteerAccepted = 6,

        VolunteerRejected = 7,

    }
}
