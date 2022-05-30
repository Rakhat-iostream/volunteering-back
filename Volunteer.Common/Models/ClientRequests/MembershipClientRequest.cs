using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain.Enum;

namespace Volunteer.Common.Models.ClientRequests
{
    public class MembershipClientRequest
    {
        public int? MembershipId { get; set; }
        public MembershipStatus? MembershipStatus { get; set; }
    }
}
