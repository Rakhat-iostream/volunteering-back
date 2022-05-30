using Microsoft.EntityFrameworkCore;
using Volunteer.Common.Models.Domain;
using Volunteer.Dal.SqlContext.Configuration;

namespace Volunteer.Dal.SqlContext
{
    public class VolContext : DbContext
    {
        public VolContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RefreshTokenConfiguration())
                //.ApplyConfiguration(new SmsCodeConfiguration())
                .ApplyConfiguration(new UserConfiguration());
        }


        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        //public DbSet<SmsCode> SmsCodes { get; set; }
        public DbSet<Common.Models.Domain.Volunteer> Volunteers { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Membership> Memberships { get; set; }
        public DbSet<News> News { get; set; }

    }
}
