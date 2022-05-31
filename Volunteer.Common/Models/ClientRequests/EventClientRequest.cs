using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.Domain.Enum;

namespace Volunteer.Common.Models.ClientRequests
{
    public class EventClientRequest : PageRequest
    {
        public int? EventId { get; set; }
        public Region? Region { get; set; }
        public VolunteeringCategories? VolunteeringCategory { get; set; }
        public bool? IsFinished { get; set; }
    }
}
