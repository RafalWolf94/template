namespace Web.Domain.Models.ValueObjects;

public readonly record struct AppUserDataId : IValueObject<Guid>
{
    private readonly Guid _value;

    public Guid Value
    {
        get => _value;
        init => _value = Validate(value);
    }

    private static Guid Validate(Guid value)
    {
        if (Guid.Empty == value)
            throw new ArgumentException("AppUserDataId cannot be empty.", nameof(value));

        return value;
    }

    public AppUserDataId(Guid value)
    {
        _value = Validate(value);
    }

    public static bool TryParse(Guid value, out AppUserId email)
    {
        email = new AppUserId(value);
        return true;
    }
}