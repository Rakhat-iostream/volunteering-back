using System;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.Domain.Enum;

namespace Volunteer.Common.Models.DTOs.Events
{
    public class EventUpdateDto
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public Region Region { get; set; }
        public string? Location { get; set; }
        public string? Image { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime EndDate { get; set; }
        public VolunteeringCategories VolunteeringCategory { get; set; }
        //public bool IsFinished { get; set; }
    }
}
