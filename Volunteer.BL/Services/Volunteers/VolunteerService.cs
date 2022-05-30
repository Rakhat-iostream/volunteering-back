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

        public async Task<Common.Models.Domain.Volunteer> GetByUserId(int userId)
        {
            var entity = await _volunteerRepository.GetByUserId(userId);
            return entity;
        }

        public async Task<VolunteerProfileDto> GetById(int volunteerId)
        {
            var entity = await _volunteerRepository.GetById(volunteerId);

            var model = _mapper.Map<VolunteerProfileDto>(entity);

            var user = _userRepository.GetAsync(entity.UserId ?? 0).Result;

            model.Login = user.Login;
            model.Phone = user.Phone;
            model.Email = user.Email;
            model.Avatar = user.Avatar;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;

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

            profile.UserId = user.Id;
            profile.Avatar = user.Avatar;

            user.Role = Common.Models.Domain.Enum.UserRoles.Volunteer;
            await _userRepository.UpdateAsync(user);

            return profile;
        }

        public async Task<VolunteerProfileDto> UpdateAsync(VolunteerUpdateDto dto, User user)
        {
            var volunteer = _mapper.Map<VolunteerUpdateDto, Common.Models.Domain.Volunteer>(dto);

            var result = await _volunteerRepository.UpdateAsync(volunteer);

            user.Login = dto.Login ?? user.Login;
            user.Email = dto.Email ?? user.Email;
            user.Phone = dto.Phone ?? user.Phone;
            await _userRepository.UpdateAsync(user);

            var profile = _mapper.Map<VolunteerProfileDto>(volunteer);
            return profile;
        }
    }
}
