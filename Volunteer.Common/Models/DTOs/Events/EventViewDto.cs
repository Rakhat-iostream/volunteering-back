using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;

namespace Volunteer.Common.Models.DTOs.Events
{
    public class EventViewDto
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime Deadline { get; set; }
        public DateTime EndDate { get; set; }
        public VolunteeringCategories VolunteeringCategory { get; set; }
        public bool IsFinished { get; set; }
        public int? OrganizationId { get; set; }
        public List<int>? VolunteerIds { get; set; }
    }
}
