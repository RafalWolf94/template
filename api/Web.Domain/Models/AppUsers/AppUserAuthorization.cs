using Web.Domain.Models.ValueObjects;

namespace Web.Domain.Models.AppUsers;

public sealed record AppUserAuthorization
{
    public AppUserAuthorizationId AppUserAuthorizationId { get; set; }
    public string Password { get; private set; } = null!;
    public string? ProviderId { get; set; }
    public bool IsPasswordRandom { get; private set; }
    public AppUserId AppUserId { get; set; }

    public static AppUserAuthorization Create(string password, bool isRandom) => new()
    {
        Password = password,
        IsPasswordRandom = isRandom
    };

    public void UpdatePassword(string password)
    {
        Password = password;
        IsPasswordRandom = false;
    }
}