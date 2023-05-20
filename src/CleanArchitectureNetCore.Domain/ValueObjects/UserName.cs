using CSharpFunctionalExtensions;

namespace CleanArchitectureNetCore.Domain.ValueObjects;

public class UserName : ValueObject<UserName>
{
    private UserName(string userName)
    {
        Value = userName;
    }

    public string Value { get; set; }

    public static Result<UserName> Create(string userName)
    {
        userName = (userName ?? string.Empty).Trim();

        if (userName.Length == 0)
            return Result.Failure<UserName>("UserName should not be empty");

        return Result.Success(new UserName(userName));
    }

    protected override bool EqualsCore(UserName other)
    {
        return Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
    }

    protected override int GetHashCodeCore()
    {
        return Value.GetHashCode();
    }

    public static implicit operator string(UserName userName)
    {
        return userName.Value;
    }

    public static explicit operator UserName(string userName)
    {
        return Create(userName).Value;
    }
}