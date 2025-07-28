using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Domain.Models.AppUsers;
using Web.Infrastructure.Common.Persistence.Configurations.Converters;

namespace Web.Infrastructure.Common.Persistence.Configurations.AppUserConfigurations;

public class AppUserDataConfiguration : IEntityTypeConfiguration<AppUserData>
{
    public void Configure(EntityTypeBuilder<AppUserData> builder)
    {
        builder.ToTable("app_user_data");

        builder.Property(e => e.AppUserDataId)
            .HasConversion(ValueObjectConverters.AppUserDataIdConverter)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(e => e.FirstName)
            .HasColumnName("first_name")
            .HasMaxLength(255);

        builder.Property(e => e.LastName)
            .HasColumnName("last_name")
            .HasMaxLength(255);

        builder.Property(e => e.City)
            .HasColumnName("city")
            .HasMaxLength(255);

        builder.Property(e => e.PhoneNumber)
            .HasColumnName("phone_number")
            .HasMaxLength(20);

        builder.Property(e => e.PhoneNumberPrefix)
            .HasColumnName("phone_number_prefix")
            .HasMaxLength(10);

        builder.Property(e => e.AppUserId)
            .HasConversion(ValueObjectConverters.AppUserIdConverter)
            .HasColumnName("app_user_id")
            .HasMaxLength(255)
            .IsRequired();
    }
}