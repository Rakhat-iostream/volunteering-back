using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vol.Domain.Users;

namespace Vol.EntityFrameworkCore
{
    public static class VolDbContextModelCreatingExtensions
    {

        public static void ConfigureVol(this ModelBuilder builder)
        {
            builder.Entity<User>(b =>
            {
                b.Property(x => x.Surname);
                b.Property(x => x.FullName);
                b.Property(x => x.PartnerId);
                b.Property(x => x.PreferLanguage).HasMaxLength(6);
                b.Property(x => x.IsExternal);

                b.HasIndex(x => x.PartnerId);
            });
        }
    }
}
