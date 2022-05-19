using System.ComponentModel;

namespace Volunteer.Common.Models.Domain.Enum
{
    public enum UserRoles
    {
        None,
        [Description("User")]
        User,

        [Description("Volunteer")]
        Volunteer,

        [Description("OrganizationMember")]
        OrganizationMember,

        [Description("Administrator")]
        Administrator
    }
}
