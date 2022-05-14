using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volunteer.Common.Repositories.Auth;
using Volunteer.Common.Repositories.Users;
using Volunteer.Dal.Repositories;
using Volunteer.Dal.Repositories.Users;
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
            var connecionString = configuration.GetConnectionString("NMDatabase");
            services.AddDbContext<VolContext>(options =>
            {
                options.UseNpgsql(connecionString);
            });
        }

        private static void AddDependenciesToContainer(IServiceCollection services)
        {
            //services.AddTransient<ISmsTokenRepository, SmsTokenRepository>();
            services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddTransient<IUserRepository, UserRepository>();


        }
    }
}
