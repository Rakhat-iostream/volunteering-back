using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vol.Domain.Volunteers
{
    public class VolunteeringCategory
    {
        [Key]
        public int CategoryId { get; set; }
        public string Description { get; set; }
    }
}
