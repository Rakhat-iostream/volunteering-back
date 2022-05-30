using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain.Enum;

namespace Volunteer.Common.Models.Domain.ViewModels
{
    public class MembershipViewModel
    {
        public int MembershipId { get; set; }

        public int VolunteerId { get; set; }
        public MembershipStatus MembershipStatus { get; set; }

        public int? OrganizationId { get; set; }
    }
}
