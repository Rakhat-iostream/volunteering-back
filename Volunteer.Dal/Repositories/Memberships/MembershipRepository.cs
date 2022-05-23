using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Repositories.Memberships;
using Volunteer.Dal.SqlContext;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.Domain.ViewModels;
using AutoMapper;

namespace Volunteer.Dal.Repositories.Memberships
{
    public class MembershipRepository : IMembershipRepository
    {
        private readonly VolContext _db;
        private readonly IMapper _mapper;
        public MembershipRepository(VolContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<Membership> AddMembership(Membership model, int volunteerId)
        {
            var membership = _mapper.Map<Membership>(model);

            membership.MembershipId = new int();

            if (membership.VolunteerIds == null)
            {
                membership.VolunteerIds = new List<int> { };
                membership.VolunteerIds.Add(volunteerId);
                await _db.SaveChangesAsync();
            }

            else if (!membership.VolunteerIds.Contains(volunteerId))
            {
                membership.VolunteerIds.Add(volunteerId);
                await _db.SaveChangesAsync();
            }
            else
            {
                throw new Exception("You have already joined to this event");
            }

            membership.OrganizationId = model.OrganizationId;
            membership.MembershipStatus = Common.Models.Domain.Enum.MembershipStatus.NeedOrganizationApprove;
            await _db.Memberships.AddAsync(membership);
            await _db.SaveChangesAsync();

            return membership;
        }

        /*public void ChangeRequest(MembershipViewModel model)
        {
            var entity = _db.Memberships.FirstOrDefault(x => x.MembershipId.Equals(model.MembershipId));

            if (entity == null) return;

            entity.MembershipStatus = model.MembershipStatus;
            entity.
        }*/
    }
}
