using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vol.Domain.Users;

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
