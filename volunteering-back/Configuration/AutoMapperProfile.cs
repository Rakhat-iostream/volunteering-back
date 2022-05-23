using AutoMapper;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.Domain.ViewModels;
using Volunteer.Common.Models.DTOs.Events;
using Volunteer.Common.Models.DTOs.Organizations;
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
            CreateMap<User, Common.Models.Domain.Volunteer>().ReverseMap();

            CreateMap<Common.Models.Domain.Volunteer, VolunteerAddDto>().ReverseMap();
            CreateMap<Common.Models.Domain.Volunteer, VolunteerUpdateDto>().ReverseMap();
            CreateMap<Common.Models.Domain.Volunteer, VolunteerProfileDto>().ReverseMap();

            CreateMap<Organization, OrganizationProfileDto>().ReverseMap();
            CreateMap<Organization, OrganizationAddDto>().ReverseMap();
            CreateMap<Organization, OrganizationUpdateDto>().ReverseMap();

            CreateMap<Event, EventAddDto>().ReverseMap();
            CreateMap<Event, EventViewDto>().ReverseMap();
            CreateMap<Event, EventUpdateDto>().ReverseMap();

            CreateMap<Membership, MembershipViewModel>().ReverseMap();
        }
    }
}
