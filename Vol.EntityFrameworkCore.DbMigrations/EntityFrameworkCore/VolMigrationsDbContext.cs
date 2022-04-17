using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Vol.Users;

namespace Vol.EntityFrameworkCore
{
    public class VolMigrationsDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public VolMigrationsDbContext(DbContextOptions<VolMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ConfigureVol();
        }
    }
}
