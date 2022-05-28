using System.Collections.Generic;
using System.Threading.Tasks;
using Volunteer.Common.Models.ClientRequests;
using Volunteer.Common.Models.Domain;

namespace Volunteer.Common.Repositories.Memberships
{
    public interface IMembershipRepository
    {
        public Task<Membership> AddMembership(Membership model, int volunteerId);
        public Task<Membership> ChangeRequest(MembershipClientRequest clientRequest);
        public ICollection<Models.Domain.Volunteer> GetCandidates(int organizationId);
        public void InviteMembership(int organizationId, int volunteerId);
        public Task<Membership> VolunteerAnswer(MembershipClientRequest clientRequest);
    }
}
