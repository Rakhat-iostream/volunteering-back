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

        public async Task<VolunteerProfileDto> GetById(int volunteerId)
        {
            var entity = await _volunteerRepository.GetById(volunteerId);

            var model = _mapper.Map<VolunteerProfileDto>(entity);

            return model;
        }

        public async Task<VolunteerProfileDto> CreateAsync(VolunteerAddDto dto ,User user)
        {
            Common.Models.Domain.Volunteer volunteer = new Common.Models.Domain.Volunteer();
            volunteer.VolunteerId = new int();
            volunteer.UserId = user.Id;
            volunteer.Region = dto.Region;
            volunteer.Sex = dto.Sex;
            volunteer.Experience = dto.Experience;
            volunteer.VolunteeringCategories = dto.VolunteeringCategories;
            volunteer.BirthDate = dto.BirthDate;
            volunteer.Description = dto.Description;

            await _volunteerRepository.CreateAsync(volunteer);

            var profile = _mapper.Map<VolunteerProfileDto>(volunteer);

            user.Role = Common.Models.Domain.Enum.UserRoles.Volunteer;
            await _userRepository.UpdateAsync(user);

            return profile;
        }

        public async Task<VolunteerProfileDto> UpdateAsync(VolunteerUpdateDto dto, User user)
        {
            var volunteer = await this.GetById(dto.VolunteerId);

            var mapped = _mapper.Map<Common.Models.Domain.Volunteer>(dto);

            mapped.BirthDate = dto.BirthDate ?? mapped.BirthDate;
            mapped.Description = dto.Description ?? mapped.Description;
            mapped.Region = dto.Region ?? mapped.Region;
            mapped.Sex = dto.Sex ?? mapped.Sex;
            mapped.Experience = dto.Experience ?? mapped.Experience;
            mapped.VolunteeringCategories = dto.VolunteeringCategories;
            mapped.UserId = mapped.UserId;
            await _volunteerRepository.UpdateAsync(mapped);

            user.Login = dto.Login ?? user.Login;
            user.Email = dto.Email ?? user.Email;
            user.Phone = dto.Phone ?? user.Phone;
            //var profile = _mapper.Map<VolunteerProfileDto>(user);
            await _userRepository.UpdateAsync(user);


            var profile = _mapper.Map<VolunteerProfileDto>(mapped);

            return profile;
        }
    }
}
