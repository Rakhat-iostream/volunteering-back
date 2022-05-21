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
    public class VolunteerAddDto
    {
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public bool Sex { get; set; }

        public Region Region { get; set; }

        [Required]
        public VolunteeringCategories[] VolunteeringCategories { get; set; }
        [Required]
        public int Experience { get; set; }

        public string Description { get; set; }

        //public int UserId { get; set; }
    }
}
