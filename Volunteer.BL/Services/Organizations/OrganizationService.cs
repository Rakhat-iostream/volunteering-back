using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.DTOs.Organizations;
using Volunteer.Common.Repositories.Organizations;
using Volunteer.Common.Repositories.Users;
using Volunteer.Common.Services.Organizations;

namespace Volunteer.BL.Services.Organizations
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationRepository _organizationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public OrganizationService(IOrganizationRepository organizationRepository, 
            IUserRepository userRepository,
            IMapper mapper)
        {
            _organizationRepository = organizationRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<OrganizationProfileDto> CreateAsync(OrganizationAddDto dto, User user)
        {
            Organization organization = new Organization();
            organization.OrganizationId = new int();
            organization.OrganizationName = dto.OrganizationName;
            organization.OrganizationTypes = dto.OrganizationTypes;
            organization.OrganizedDate = dto.OrganizedDate;
            organization.Experience = dto.Experience;
            organization.Location = dto.Location;
            organization.Description = dto.Description;
            organization.Region = dto.Region;
            organization.UserId = dto.UserId;
            organization.VolunteeringCategories = dto.VolunteeringCategories;
            organization.CEO = dto.CEO;

            await _organizationRepository.CreateAsync(organization);

            var profile = _mapper.Map<OrganizationProfileDto>(organization);

            user.Role = Common.Models.Domain.Enum.UserRoles.OrganizationAdmin;
            await _userRepository.UpdateAsync(user);
            
            return profile;
        }
    }
}
