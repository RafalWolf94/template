using Web.Domain.Enums;
using Web.Domain.Models.ValueObjects;

namespace Web.Domain.Models.AppUsers;

public sealed record AppUser
{
    public AppUserId AppUserId { get; init; }
    public Email Email { get; private set; }
    public DateTime CreatedAt { get; init; }
    public DateTime? LastLogin { get; init; }
    public DateTime? ModifiedAt { get; init; }
    public bool AcceptTerms { get; private set; }
    public bool IsActive { get; private set; }
    public bool IsLocked { get; private set; }
    public RoleEnum Role { get; init; } = RoleEnum.User;

    public AppUserAuthorization AppUserAuthorization { get; init; }
    public AppUserData AppUserData { get; init; }

    private AppUser()
    {
    }

    public static AppUser Create(
        Email email,
        bool acceptTerms,
        AppUserAuthorization appUserAuthorization,
        AppUserData appUserData) =>
        new(
            email,
            acceptTerms,
            appUserAuthorization,
            appUserData);

    private AppUser(
        Email email,
        bool acceptTerms,
        AppUserAuthorization appUserAuthorization,
        AppUserData appUserData)
    {
        Email = email;
        AcceptTerms = acceptTerms;
        AppUserAuthorization = appUserAuthorization;
        AppUserData = appUserData;
        CreatedAt = DateTime.Now;
        IsLocked = false;
    }

    public string GetFullName() => $"{AppUserData.FirstName} {AppUserData.LastName}";

    public void Activate()
    {
        IsActive = true;
    }
}