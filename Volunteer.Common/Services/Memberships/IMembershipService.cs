using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.Domain.ViewModels;

namespace Volunteer.Common.Services.Memberships
{
    public interface IMembershipService
    {
        public Task<Membership> AddMembership(MembershipViewModel model, int volunteerId);
    }
}
