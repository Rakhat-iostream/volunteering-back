using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.DTOs.Volunteers;
using Volunteer.Common.Repositories.Users;
using Volunteer.Common.Repositories.Volunteers;
using Volunteer.Common.Services.Users;
using Volunteer.Common.Services.Volunteers;

namespace Volunteer.BL.Services.Volunteers
{
    public class VolunteerService : IVolunteerService
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public VolunteerService(IVolunteerRepository volunteerRepository, 
            IMapper mapper,
            IUserRepository userRepository)
        {
            _volunteerRepository = volunteerRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<VolunteerProfileDto> CreateAsync(VolunteerAddDto dto ,User user)
        {
            Common.Models.Domain.Volunteer volunteer = new Common.Models.Domain.Volunteer();
            volunteer.VolunteerId = new int();
            volunteer.UserId = dto.UserId;
            volunteer.Region = dto.Region;
            volunteer.Sex = dto.Sex;
            volunteer.Experience = dto.Experience;
            volunteer.VolunteeringCategories = dto.VolunteeringCategories;
            volunteer.BirthDate = dto.BirthDate;
            volunteer.Description = dto.Description;

            await _volunteerRepository.CreateAsync(volunteer);

            var profile = _mapper.Map<VolunteerProfileDto>(user);

            user.Role = Common.Models.Domain.Enum.UserRoles.Volunteer;
            await _userRepository.UpdateAsync(user);

            return profile;
        }
    }
}
