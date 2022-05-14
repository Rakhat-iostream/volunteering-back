using System.ComponentModel;

namespace Volunteer.Common.Models.Domain.Enum
{
    public enum UserRoles
    {
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
