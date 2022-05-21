using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.Domain.Enum;

namespace Volunteer.Common.Models.DTOs.Volunteers
{
    public class VolunteerUpdateDto
    {
        public int VolunteerId { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool? Sex { get; set; }

        public Region? Region { get; set; }

        public VolunteeringCategories[]? VolunteeringCategories { get; set; }
        public int? Experience { get; set; }

        public string? Login { get; set; }

        [Phone]
        public string? Phone { get; set; }
        [EmailAddress]
        public string? Email { get; set; }

        public string? Description { get; set; }

    }
}
