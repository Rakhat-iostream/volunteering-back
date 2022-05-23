using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;

namespace Volunteer.Common.Repositories.Memberships
{
    public interface IMembershipRepository
    {
        public Task<Membership> AddMembership(Membership model, int volunteerId);
    }
}
