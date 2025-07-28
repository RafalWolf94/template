using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Web.Domain.Models.AppUsers;
using Web.Infrastructure.Common.Persistence.Configurations.Converters;

namespace Web.Infrastructure.Common.Persistence.Configurations.AppUserConfigurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.ToTable("app_user");
        builder.HasKey(e => e.AppUserId);


        builder.Property(u => u.AppUserId)
            .HasConversion(ValueObjectConverters.AppUserIdConverter)
            .HasColumnName("id");

        builder.Property(e => e.Email)
            .HasColumnName("email")
            .HasConversion(ValueObjectConverters.EmailConverter)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp");

        builder.Property(u => u.LastLogin)
            .HasColumnName("last_login")
            .HasColumnType("timestamp");

        builder.Property(u => u.ModifiedAt)
            .HasColumnName("modified_at")
            .HasColumnType("timestamp");

        builder.Property(u => u.AcceptTerms)
            .HasColumnName("accept_terms")
            .HasColumnType("boolean");

        builder.Property(u => u.IsActive)
            .HasColumnName("is_active");

        builder.Property(u => u.IsLocked)
            .HasDefaultValue(false)
            .HasColumnName("is_locked");

        builder.Property(u => u.Role)
            .HasColumnName("role")
            .HasConversion<string>();

        builder.HasOne(e => e.AppUserData)
            .WithOne()
            .HasForeignKey<AppUserData>(d => d.AppUserId);

        builder.HasOne(e => e.AppUserAuthorization)
            .WithOne()
            .HasForeignKey<AppUserAuthorization>(a => a.AppUserId);
    }
}