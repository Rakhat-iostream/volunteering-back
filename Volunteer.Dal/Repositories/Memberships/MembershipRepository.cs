﻿using System;
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
            //var exists = _db.Memberships.FirstOrDefault(x => x.OrganizationId == model.OrganizationId);

            var membership = _mapper.Map<Membership>(model);

            /*if (exists != null)
            {
                exists.VolunteerIds.Add(volunteerId);
                 _db.Update(exists);
                await _db.SaveChangesAsync();
            }
            else
            {*/
            membership.MembershipId = new int();
            membership.OrganizationId = model.OrganizationId;
            membership.VolunteerId = volunteerId;
            membership.MembershipStatus = Common.Models.Domain.Enum.MembershipStatus.NeedOrganizationApprove;
            await _db.Memberships.AddAsync(membership);
            await _db.SaveChangesAsync();
            //}

            return membership;
        }

        public async Task<Membership> ChangeRequest(MembershipClientRequest clientRequest)
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
            await _db.SaveChangesAsync();

            return request;
        }

        private Common.Models.Domain.Volunteer AddMembershipIdToVolunteer(Membership membership)
        {
            //var entity = _db.Volunteers.FirstOrDefault(x => membership.VolunteerIds.Contains(x.VolunteerId));
            var entity = _db.Volunteers.FirstOrDefault(x => x.VolunteerId == membership.VolunteerId);

            entity.MembershipId = membership.MembershipId;

            _db.Volunteers.Update(entity);
            _db.SaveChanges();

            return entity;
        }

        public ICollection<Common.Models.Domain.Volunteer> GetCandidates(int organizationId)
        {
            var memberships = _db.Memberships.Where(x => x.OrganizationId == organizationId 
            && x.MembershipStatus == Common.Models.Domain.Enum.MembershipStatus.NeedOrganizationApprove).ToList();

            //var ids = membership.VolunteerIds;
            //var id = ids.FirstOrDefault();
            //var volunteer = _db.Volunteers.Where(x => ids.Contains(id)).ToList();
            List <Common.Models.Domain.Volunteer > volunteers = new List<Common.Models.Domain.Volunteer>();

            foreach (var membership in memberships)
            {
                var volunteer = _db.Volunteers.Where(x => x.VolunteerId == membership.VolunteerId).ToList();
                volunteers = volunteer;
            }
            return volunteers;
        }
    }
}