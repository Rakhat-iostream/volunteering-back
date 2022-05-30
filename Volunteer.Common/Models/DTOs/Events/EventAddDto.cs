using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.Domain.Enum;

namespace Volunteer.Common.Models.DTOs.Events
{
    public class EventAddDto
    {
        public string EventName { get; set; }
        public Region Region { get; set; }
        public string? Location { get; set; }
        public string? Image { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime EndDate { get; set; }
        public VolunteeringCategories VolunteeringCategory { get; set; }
        //public bool IsFinished { get; set; }
    }
}
