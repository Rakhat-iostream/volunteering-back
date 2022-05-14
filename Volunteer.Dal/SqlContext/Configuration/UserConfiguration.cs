using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volunteer.Common.Models.Domain;
using Volunteer.Common.Models.Domain.Enum;

namespace Volunteer.Dal.SqlContext.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(u => u.Login).IsUnique();
            builder.HasIndex(u => u.Phone).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();

            builder.HasData(
                new User
                {
                    Id = 1,
                    Login = "admin",
                    PasswordHash = "admin",
                    Email = "admin@admin.com",
                    Phone = "+77071234567",
                    Role = UserRoles.Administrator,
                    Status = UserStatus.Active,
                    FullName = "Admin Admin"
                });
        }
    }
}
