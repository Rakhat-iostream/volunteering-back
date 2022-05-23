using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volunteer.Common.Models.Domain.Enum;

namespace Volunteer.Common.Models.Domain
{
    public class Membership
    {
        [Key]
        public int MembershipId { get; set; }

        public List<int>? VolunteerIds { get; set; }
        public MembershipStatus MembershipStatus { get; set; }

        public int? OrganizationId { get; set; }

        [ForeignKey(nameof(OrganizationId))]
        public virtual Organization Organization { get; set; }
    }
}
