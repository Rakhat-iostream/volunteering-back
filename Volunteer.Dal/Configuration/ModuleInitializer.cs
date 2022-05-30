using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volunteer.Common.Repositories;
using Volunteer.Common.Repositories.Auth;
using Volunteer.Common.Repositories.Events;
using Volunteer.Common.Repositories.Memberships;
using Volunteer.Common.Repositories.Organizations;
using Volunteer.Common.Repositories.Users;
using Volunteer.Common.Repositories.Volunteers;
using Volunteer.Dal.Repositories;
using Volunteer.Dal.Repositories.Events;
using Volunteer.Dal.Repositories.Memberships;
using Volunteer.Dal.Repositories.Organizations;
using Volunteer.Dal.Repositories.Users;
using Volunteer.Dal.Repositories.Volunteers;
using Volunteer.Dal.SqlContext;

namespace Volunteer.Dal.Configuration
{
    public static class ModuleInitializer
    {
        public static IServiceCollection ConfigureDal(this IServiceCollection services, IConfiguration configuration)
        {
            SetSettings(services, configuration);
            AddDependenciesToContainer(services);

            return services;
        }

        private static void SetSettings(IServiceCollection services, IConfiguration configuration)
        {
            var connecionString = configuration.GetConnectionString("Database");
            services.AddDbContext<VolContext>(options =>
            {
                options.UseNpgsql(connecionString);
            });
        }

        private static void AddDependenciesToContainer(IServiceCollection services)
        {
            services.AddTransient<ISmsTokenRepository, SmsTokenRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IVolunteerRepository, VolunteerRepository>();
            services.AddTransient<IOrganizationRepository, OrganizationRepository>();
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<IMembershipRepository, MembershipRepository>();
            services.AddTransient<INewsRepository, NewsRepository>();
        }
    }
}
