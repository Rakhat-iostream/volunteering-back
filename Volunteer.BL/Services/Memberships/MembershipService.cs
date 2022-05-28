
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volunteer.Common.Models;
using Volunteer.Common.Models.ClientRequests;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.Domain.ViewModels;
using Volunteer.Common.Models.DTOs.Memberships;
using Volunteer.Common.Models.DTOs.Volunteers;
using Volunteer.Common.Repositories.Memberships;
using Volunteer.Common.Services.Memberships;

namespace Volunteer.BL.Services.Memberships
{
    public class MembershipService : IMembershipService
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IMapper _mapper;

        public MembershipService(IMembershipRepository membershipRepository, IMapper mapper)
        {
            _membershipRepository = membershipRepository;
            _mapper = mapper;
        }

        public async Task<Membership> AddMembership(MembershipAddDto model, int volunteerId)
        {
            var entity = _mapper.Map<Membership>(model);
            await _membershipRepository.AddMembership(entity, volunteerId);
            return entity;
        }

        public async Task<Membership> ChangeMembershipStatus(MembershipClientRequest clientRequest)
        {
            var model =  await _membershipRepository.ChangeRequest(clientRequest);
            return model;
        }

        public async Task<PageResponse<VolunteerProfileDto>> GetCandidates(PageRequest request, int organizationId)
        {
            var candidates = _membershipRepository.GetCandidates(organizationId);

            var total = candidates.Count();
            var model = _mapper.Map<List<VolunteerProfileDto>>(candidates);

            var result = model.Skip(request.Skip).Take(request.Take);

            return new PageResponse<VolunteerProfileDto>
            {
                Total = total,
                Result = result
            };

        }

        public void InviteMembership(int organizationId, int volunteerId)
        {
              _membershipRepository.InviteMembership(organizationId, volunteerId);
        }

        public async Task<Membership> ChangeVolunteerAnswer(MembershipClientRequest clientRequest)
        {
            var model = await _membershipRepository.VolunteerAnswer(clientRequest);
            return model;
        }

        public async Task<PageResponse<MembershipViewModel>> InvitationsList(FilterMembershipRequest request, int volunteerId)
        {
            var invitations = _membershipRepository.InvitationsList(request, volunteerId);

            var total = invitations.Count();
            var model = _mapper.Map<List<MembershipViewModel>>(invitations);

            var result = model.Skip(request.Skip).Take(request.Take);

            return new PageResponse<MembershipViewModel>
            {
                Total = total,
                Result = result
            };

        }

    }
}
