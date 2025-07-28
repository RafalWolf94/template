using System.Security.Claims;
using Web.Domain.Authorization;
using Web.Domain.Models.ValueObjects;

namespace Web.Api.TechnicalStuff.Authorization;

public class AuthenticatedUserService : IAuthenticatedUserService
{
    public AppUserId AppUserId { get; }
    public Guid? TutorId { get; }

    public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
    {
        AppUserId = FindValue(httpContextAccessor, "uid", str => new AppUserId(str));
        // TutorId = FindValue(httpContextAccessor, "tutorId", Guid.TryParse);
    }

    private static T FindValue<T>(IHttpContextAccessor httpContextAccessor, string claimType, Func<string, (bool success, T value)> tryParse)
    {
        var strValue = httpContextAccessor.HttpContext?.User.FindFirstValue(claimType);
        if (strValue == null) return default;

        var (success, value) = tryParse(strValue);
        return success ? value : default;
    }

    private static T FindValue<T>(IHttpContextAccessor httpContextAccessor, string claimType, Func<Guid, T> parse)
    {
        var strValue = httpContextAccessor.HttpContext?.User.FindFirstValue(claimType);
        if (strValue == null) return default;
        
        var guid = Guid.Parse(strValue);

        try
        {
            return parse(guid);
        }
        catch
        {
            return default;
        }
    }
}