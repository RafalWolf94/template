using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Domain.Models.AppUsers;
using Web.Infrastructure.Common.Persistence.Configurations.Converters;

namespace Web.Infrastructure.Common.Persistence.Configurations.AppUserConfigurations;

public class AppUserAuthorizationConfiguration : IEntityTypeConfiguration<AppUserAuthorization>
{
    public void Configure(EntityTypeBuilder<AppUserAuthorization> builder)
    {
        builder.ToTable("app_user_authorization");

        builder.HasKey(e => e.AppUserAuthorizationId);

        builder.Property(e => e.AppUserAuthorizationId)
            .HasConversion(ValueObjectConverters.AppUserAuthorizationIdConverter)
            .HasColumnName("id")
            .IsRequired();

        builder.Property(e => e.Password)
            .HasColumnName("password")
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(e => e.ProviderId)
            .HasColumnName("provider_id")
            .HasMaxLength(255);

        builder.Property(e => e.AppUserId)
            .HasColumnName("app_user_id")
            .HasConversion(ValueObjectConverters.AppUserIdConverter)
            .HasColumnType("char(36)")
            .IsRequired();

        builder.Property(e => e.IsPasswordRandom)
            .HasColumnName("is_password_random")
            .HasColumnType("boolean")
            .IsRequired();

        builder.HasIndex(e => e.AppUserId)
            .IsUnique();
    }
}