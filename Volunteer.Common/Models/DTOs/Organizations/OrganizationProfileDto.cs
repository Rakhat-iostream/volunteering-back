using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.Domain.Enum;

namespace Volunteer.Common.Models.DTOs.Organizations
{
    public class OrganizationProfileDto
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string Location { get; set; }
        public string Logo { get; set; }
        public Region Region { get; set; }

        public VolunteeringCategories[] VolunteeringCategories { get; set; }
        public OrganizationTypes[] OrganizationTypes { get; set; }
        public string? CEO { get; set; }
        public DateTime? OrganizedDate { get; set; }
        public int Experience { get; set; }

        public string Description { get; set; }

        public string Login { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }

    }
}
