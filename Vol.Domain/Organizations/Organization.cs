using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vol.Domain.Organizations
{
    public class Organization
    {
        [Key]
        public int OrganizationId { get; set;}
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CEO { get; set; }
        public IFo
    }
}
