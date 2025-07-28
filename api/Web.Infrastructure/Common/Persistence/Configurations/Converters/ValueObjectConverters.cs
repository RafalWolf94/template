using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Web.Domain.Models.ValueObjects;

namespace Web.Infrastructure.Common.Persistence.Configurations.Converters;

public static class ValueObjectConverters
{
    public static ValueConverter<AppUserId, Guid> AppUserIdConverter =>
        new(
            id => id.Value,
            value => new AppUserId(value));
    
    public static ValueConverter<AppUserDataId, Guid> AppUserDataIdConverter =>
        new(
            id => id.Value,
            value => new AppUserDataId(value));
    
    public static ValueConverter<AppUserAuthorizationId, Guid> AppUserAuthorizationIdConverter =>
        new(
            id => id.Value,
            value => new AppUserAuthorizationId(value));
            
    public static ValueConverter<Email, string> EmailConverter =>
        new ValueConverter<Email, string>(
            email => email.Value,
            value => new Email(value));
            
    // Add more converters as needed
}
