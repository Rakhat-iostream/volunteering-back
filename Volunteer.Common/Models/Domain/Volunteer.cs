using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volunteer.Common.Models.Domain.Enum;

namespace Volunteer.Common.Models.Domain
{
    public class Volunteer
    {
        [Key]
        public int VolunteerId { get; set; }

        public DateTime? BirthDate { get; set; }
        public bool? Sex { get; set; }
        public Region? Region { get; set; }
        public VolunteeringCategories[] VolunteeringCategories { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? Experience { get; set; }
        //public Membership[] Memberships { get; set; }
        public string? Description { get; set; }

        public int? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public int? MembershipId { get; set; }
        [ForeignKey(nameof(MembershipId))]
        public virtual Membership Membership { get; set; }
    }
}
