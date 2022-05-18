using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volunteer.Common.Models.Domain;

namespace Volunteer.Dal.SqlContext.Configuration
{
    public class SmsCodeConfiguration : IEntityTypeConfiguration<SmsCode>
    {
        public void Configure(EntityTypeBuilder<SmsCode> builder)
        {
            builder.Property(x => x.UserId)
                .IsRequired();
            builder.Property(x => x.Code)
                .IsRequired();
        }
    }
}
