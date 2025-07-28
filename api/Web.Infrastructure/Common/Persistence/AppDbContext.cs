using Microsoft.EntityFrameworkCore;
using Web.Domain.Models.AppUsers;
using Web.Infrastructure.Common.Persistence.Configurations.AppUserConfigurations;

namespace Web.Infrastructure.Common.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<AppUser> AppUser { get; set; }
    public DbSet<AppUserData> AppUserData { get; set; }
    public DbSet<AppUserAuthorization> AppUserAuthorization { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AppUserConfiguration());
        modelBuilder.ApplyConfiguration(new AppUserDataConfiguration());
        modelBuilder.ApplyConfiguration(new AppUserAuthorizationConfiguration());
    }
}