using AutoMapper;
using Vol.Users;
using Vol.V1.Account;
using Vol.V1.Users;

namespace Vol
{
    public class VolApplicationAutoMapperProfile : Profile
    {
        public VolApplicationAutoMapperProfile()
        {
            CreateMap<User, UserProfileDto>();
            CreateMap<User, UserV1Dto>();

            CreateMap<User, IdentityUserDto>();
        }
    }
}
