using Microsoft.EntityFrameworkCore;
using Vol.Users;

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
