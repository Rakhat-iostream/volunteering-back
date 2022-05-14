using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vol.Users;

namespace Vol.Domain.Volunteers
{
    public class VolunteerProfile
    {
        public VolunteeringCategory[] VolunteeringCategory { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Experience { get; set; }
        public int[] Membership { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

    }
}
