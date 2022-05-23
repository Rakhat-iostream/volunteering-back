using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.Domain.ViewModels;
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

        public async Task<Membership> AddMembership(MembershipViewModel model, int volunteerId)
        {
            var entity = _mapper.Map<Membership>(model);
            await _membershipRepository.AddMembership(entity, volunteerId);
            return entity;
        }
    }
}
