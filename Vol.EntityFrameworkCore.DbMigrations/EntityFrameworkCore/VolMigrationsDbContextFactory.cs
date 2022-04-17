using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Vol.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class VolMigrationsDbContextFactory : IDesignTimeDbContextFactory<VolMigrationsDbContext>
    {
        public VolMigrationsDbContext CreateDbContext(string[] args)
        {

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<VolMigrationsDbContext>()
                .UseNpgsql(configuration.GetConnectionString("Default"));

            return new VolMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Vol.DbMigrator/"))
                .AddJsonFile("appsettings.Development.json", optional: false);

            return builder.Build();
        }
    }
}
