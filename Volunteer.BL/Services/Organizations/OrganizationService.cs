﻿using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volunteer.Common.Models;
using Volunteer.Common.Models.ClientRequests;
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

        public async Task<OrganizationProfileDto> GetAsync(int organizationId)
        {
            var organizations = await _organizationRepository.GetAsync(organizationId);
            return _mapper.Map<Organization, OrganizationProfileDto>(organizations);
        }

        public async Task<Organization> GetByUserId(int userId)
        {
            var entity = await _organizationRepository.GetByUserId(userId);
            return entity;
        }

        public async Task<PageResponse<OrganizationProfileDto>> GetAll(FilterOrganizationRequest request)
        {
            var organizations = _organizationRepository.GetAll(request);

            var total = organizations.Count();
            var model = _mapper.Map<List<OrganizationProfileDto>>(organizations);

            var result = model.Skip(request.Skip).Take(request.Take);

            return new PageResponse<OrganizationProfileDto>
            {
                Total = total,
                Result = result
                
            };
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
            organization.UserId = user.Id;
            organization.VolunteeringCategories = dto.VolunteeringCategories;
            organization.CEO = dto.CEO;

            await _organizationRepository.CreateAsync(organization);

            var profile = _mapper.Map<OrganizationProfileDto>(organization);

            user.Role = Common.Models.Domain.Enum.UserRoles.OrganizationAdmin;
            await _userRepository.UpdateAsync(user);
            
            return profile;
        }

        public async Task<OrganizationProfileDto> UpdateAsync(OrganizationUpdateDto dto, User user)
        {
            var organization = _mapper.Map<OrganizationUpdateDto, Organization>(dto);

            var result = await _organizationRepository.UpdateAsync(organization);

            user.Login = dto.Login ?? user.Login;
            user.Email = dto.Email ?? user.Email;
            user.Phone = dto.Phone ?? user.Phone;
            await _userRepository.UpdateAsync(user);

            var profile = _mapper.Map<OrganizationProfileDto>(organization);
            return profile;
        }
    }
}