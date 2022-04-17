using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Vol.DbMigrator.Seeds;
using Vol.EntityFrameworkCore;

namespace Vol.DbMigrator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<VolMigrationsDbContext>();
                db.Database.Migrate();
                var roleSeed = scope.ServiceProvider.GetRequiredService<RoleSeedData>();
                await roleSeed.SeedRoles();
            }
            Console.WriteLine("Migration is done");
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context, logging) => logging.ClearProviders())
                .ConfigureServices((hostContext, services) =>
                {
                    var builder = new ConfigurationBuilder()
                        .AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: false);
                    var config = builder.Build();

                    services.AddDbContext<VolMigrationsDbContext>(b => b.UseNpgsql(config.GetConnectionString("Default")));
                    services.AddTransient<RoleSeedData>();
                });
    }
}
