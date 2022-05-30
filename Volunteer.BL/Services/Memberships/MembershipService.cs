
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
using Volunteer.Common.Repositories.Users;
using Volunteer.Common.Services.Memberships;

namespace Volunteer.BL.Services.Memberships
{
    public class MembershipService : IMembershipService
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public MembershipService(IMembershipRepository membershipRepository, IUserRepository userRepository, IMapper mapper)
        {
            _membershipRepository = membershipRepository;
            _userRepository = userRepository;
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

            foreach (var res in result)
            {
                var user = _userRepository.GetAsync(res.UserId).Result;
                res.Login = user.Login;
                res.Phone = user.Phone;
                res.Email = user.Email;
                res.Avatar = user.Avatar;
                res.FirstName = user.FirstName;
                res.LastName = user.LastName;
            }

            return new PageResponse<VolunteerProfileDto>
            {
                Total = total,
                Result = result
            };

        }

        public async Task<PageResponse<VolunteerProfileDto>> GetMembers(PageRequest request, int organizationId)
        {
            var candidates = _membershipRepository.GetMembers(organizationId);

            var total = candidates.Count();
            var model = _mapper.Map<List<VolunteerProfileDto>>(candidates);

            var result = model.Skip(request.Skip).Take(request.Take);

            foreach (var res in result)
            {
                var user = _userRepository.GetAsync(res.UserId).Result;
                res.Login = user.Login;
                res.Phone = user.Phone;
                res.Email = user.Email;
                res.Avatar = user.Avatar;
                res.FirstName = user.FirstName;
                res.LastName = user.LastName;
            }

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

        public async Task<PageResponse<MembershipViewModel>> GetMemberShipsByVolunteerId(FilterMembershipRequest request, int volunteerId)
        {
            var memberships = _membershipRepository.GetMemberShipsByVolunteerId(request, volunteerId);

            var total = memberships.Count();
            var model = _mapper.Map<List<MembershipViewModel>>(memberships);

            var result = model.Skip(request.Skip).Take(request.Take);

            return new PageResponse<MembershipViewModel>
            {
                Total = total,
                Result = result
            };

        }

    }
}
