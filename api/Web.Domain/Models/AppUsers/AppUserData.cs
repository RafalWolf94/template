using Web.Domain.Models.ValueObjects;

namespace Web.Domain.Models.AppUsers;

public sealed record AppUserData
{
    public AppUserDataId AppUserDataId { get; set; }
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public string PhoneNumberPrefix { get; private set; } = null!;
    public string PhoneNumber { get; private set; } = null!;

    public AppUserId AppUserId { get; init; }

    public static AppUserData Create(
        string firstName,
        string lastName,
        string city,
        string phoneNumberPrefix,
        string phoneNumber) => new()
    {
        FirstName = firstName,
        LastName = lastName,
        City = city,
        PhoneNumberPrefix = phoneNumberPrefix,
        PhoneNumber = phoneNumber,
    };

    public void Update(
        string firstName,
        string lastName,
        string city,
        string phoneNumberPrefix,
        string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        City = city;
        PhoneNumberPrefix = phoneNumberPrefix;
        PhoneNumber = phoneNumber;
    }
}