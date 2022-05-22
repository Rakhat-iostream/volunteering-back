using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.Domain.Enum;

namespace Volunteer.Common.Models.ClientRequests
{
    public class FilterOrganizationRequest : PageRequest
    {
        public Region? Region { get; set; }
        public VolunteeringCategories? VolunteeringCategories { get; set;}
        public OrganizationTypes? OrganizationTypes { get; set;}
    }
}
