using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;

namespace Volunteer.Common.Models.DTOs.Events
{
    public class EventUpdateDto
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime EndDate { get; set; }
        public VolunteeringCategories VolunteeringCategory { get; set; }
        //public bool IsFinished { get; set; }
    }
}
