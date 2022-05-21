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

        [Description("OrganizationAdmin")]
        OrganizationAdmin,

        [Description("Administrator")]
        Administrator
    }
}
