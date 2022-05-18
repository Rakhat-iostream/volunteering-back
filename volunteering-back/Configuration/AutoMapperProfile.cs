using AutoMapper;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.DTOs.User;

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

            CreateMap<User, UserProfileDto>();
        }
    }
}
