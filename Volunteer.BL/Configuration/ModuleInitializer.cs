﻿using Microsoft.Extensions.DependencyInjection;
using Volunteer.BL.Services.Auth;
using Volunteer.BL.Services.Auth.Jwt;
using Volunteer.BL.Services.Auth.Sms;
using Volunteer.Common.Services.Auth;
using Volunteer.Common.Services.Auth.Token;

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

            //services.AddScoped<IUserService, UserService>();


            return services;
        }
    }
}
