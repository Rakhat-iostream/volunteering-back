using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Volunteer.Common.Models.Domain
{
    public class Membership
    {
        [Key]
        public int MembershipId { get; set; }

        public int? VolunteerId { get; set; }
        [ForeignKey(nameof(VolunteerId))]
        public virtual Volunteer Volunteer { get; set; }

        public int? OrganizationId { get; set; }
        [ForeignKey(nameof(OrganizationId))]
        public virtual Organization Organization { get; set; }
    }
}
