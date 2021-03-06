using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Models;
using Volunteer.Common.Models.ClientRequests;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.Domain.ViewModels;
using Volunteer.Common.Models.DTOs.Memberships;
using Volunteer.Common.Models.DTOs.Volunteers;

namespace Volunteer.Common.Services.Memberships
{
    public interface IMembershipService
    {
        public Task<Membership> AddMembership(MembershipAddDto model, int volunteerId);
        public Task<Membership> ChangeMembershipStatus(MembershipClientRequest clientRequest);
        public Task<PageResponse<VolunteerProfileDto>> GetCandidates(PageRequest request, int organizationId);
        public Task<PageResponse<VolunteerProfileDto>> GetMembers(PageRequest request, int organizationId);
        public void InviteMembership(int organizationId, int volunteerId);
        public Task<Membership> ChangeVolunteerAnswer(MembershipClientRequest clientRequest);
        public Task<PageResponse<MembershipViewModel>> InvitationsList(FilterMembershipRequest request, int volunteerId);
        public Task<PageResponse<MembershipViewModel>> GetMemberShipsByVolunteerId(FilterMembershipRequest request, int volunteerId);
    }
}
