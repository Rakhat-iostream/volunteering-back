using System.ComponentModel;

namespace Volunteer.Common.Models.Domain.Enum
{
    public enum UserStatus
    {
        [Description("Inactive")]
        InActive,
        [Description("Verified")]
        Verified,
        [Description("Active")]
        Active,
        [Description("Banned")]
        Banned
    }
}
