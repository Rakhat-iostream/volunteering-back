using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volunteer.Common.Models.Domain;

namespace Volunteer.Dal.SqlContext.Configuration
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.Property(x => x.UserId)
                .IsRequired();
            builder.Property(x => x.Token)
                .IsRequired();
        }
    }
}
