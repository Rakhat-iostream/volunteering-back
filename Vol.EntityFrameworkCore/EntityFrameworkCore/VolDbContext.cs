using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Vol.Users;

namespace Vol.EntityFrameworkCore
{
    public class VolDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {

        public VolDbContext(DbContextOptions<VolDbContext> options)
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
