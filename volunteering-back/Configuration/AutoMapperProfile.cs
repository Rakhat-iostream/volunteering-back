using AutoMapper;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.DTOs.User;
using Volunteer.Common.Models.DTOs.Volunteers;

namespace Volunteer.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // User entities
            /*CreateMap<UserUpdateActionDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
            CreateMap<UserAddActionDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
            CreateMap<User, UserDto>();*/

            CreateMap<User, UserProfileDto>().ReverseMap();
            CreateMap<User, VolunteerProfileDto>().ReverseMap();
            CreateMap<Common.Models.Domain.Volunteer, VolunteerAddDto>().ReverseMap();
            CreateMap<Common.Models.Domain.Volunteer, VolunteerProfileDto>().ReverseMap();
        }
    }
}
