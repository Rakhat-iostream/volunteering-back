using Microsoft.Extensions.DependencyInjection;
using Volunteer.BL.Services.Auth;
using Volunteer.BL.Services.Auth.Jwt;
using Volunteer.BL.Services.Auth.Sms;
using Volunteer.BL.Services.Events;
using Volunteer.BL.Services.Memberships;
using Volunteer.BL.Services.Organizations;
using Volunteer.BL.Services.Users;
using Volunteer.BL.Services.Volunteers;
using Volunteer.Common.Services.Auth;
using Volunteer.Common.Services.Auth.Token;
using Volunteer.Common.Services.Events;
using Volunteer.Common.Services.Memberships;
using Volunteer.Common.Services.Organizations;
using Volunteer.Common.Services.Users;
using Volunteer.Common.Services.Volunteers;

namespace Volunteer.BL.Configuration
{
    public static class ModuleInitializer
    {
        public static IServiceCollection ConfigureBL(this IServiceCollection services)
        {
            services.AddScoped<ITokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IAccessTokenService, AccessTokenService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<ITokenValidator, TokenValidator>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ISmsTokenService, SmsTokenService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IVolunteerService, VolunteerService>();
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IMembershipService, MembershipService>();


            return services;
        }
    }
}
