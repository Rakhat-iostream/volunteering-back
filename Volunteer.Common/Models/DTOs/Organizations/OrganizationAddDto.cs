using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.Domain.Enum;

namespace Volunteer.Common.Models.DTOs.Organizations
{
    public class OrganizationAddDto
    {
        public string OrganizationName { get; set; }
        public string Location { get; set; }
        public Region? Region { get; set; }
        public VolunteeringCategories[] VolunteeringCategories { get; set; }
        public OrganizationTypes[] OrganizationTypes { get; set; }
        public string? CEO { get; set; }
        public DateTime? OrganizedDate { get; set; }
        public int Experience { get; set; }
        public string? Description { get; set; }
        public int UserId { get; set; }

        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
