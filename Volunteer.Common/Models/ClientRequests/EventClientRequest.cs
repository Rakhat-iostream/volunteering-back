using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteer.Common.Models.ClientRequests
{
    public class EventClientRequest : PageRequest
    {
        public int? EventId { get; set; }
    }
}
