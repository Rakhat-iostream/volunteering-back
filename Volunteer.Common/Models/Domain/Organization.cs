using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volunteer.Common.Models.Domain.Enum;

namespace Volunteer.Common.Models.Domain
{
    public class Organization
    {
        [Key]
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string Location { get; set; }
        public ValidationStatus ValidationStatus { get; set; }
        public Region? Region { get; set; }
        public VolunteeringCategories[] VolunteeringCategories { get; set; }
        public OrganizationTypes[] OrganizationTypes { get; set; }
        public string? CEO { get; set; }
        public DateTime? OrganizedDate { get; set; }
        public int? Experience { get; set; }
        public string? Description { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
        public string? Logo { get; set; }

    }
}
