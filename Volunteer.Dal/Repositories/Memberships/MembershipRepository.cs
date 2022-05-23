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
using Volunteer.Common.Models.ClientRequests;

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

        public Membership ChangeRequest(MembershipClientRequest clientRequest)
        {
            var request = _db.Memberships.FirstOrDefault(x => x.MembershipId.Equals(clientRequest.MembershipId));

            if (request == null)
            {
                throw new Exception("Not Found");
            }

            if (clientRequest.MembershipStatus.HasValue)
            {
                request.MembershipStatus = clientRequest.MembershipStatus ?? 0;
            }

            if (request.MembershipStatus == Common.Models.Domain.Enum.MembershipStatus.OrganizationAccepted)
            {
                var volunteer = AddMembershipIdToVolunteer(request);
            }

            _db.Memberships.Update(request);
            _db.SaveChangesAsync();

            return request;
        }

        private Common.Models.Domain.Volunteer AddMembershipIdToVolunteer(Membership membership)
        {
            var entity = _db.Volunteers.FirstOrDefault(x => membership.VolunteerIds.Contains(x.VolunteerId));

            entity.MembershipId = membership.MembershipId;

            _db.Volunteers.Update(entity);
            _db.SaveChanges();

            return entity;
        }
    }
}
