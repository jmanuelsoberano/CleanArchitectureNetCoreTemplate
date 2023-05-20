using CSharpFunctionalExtensions;

namespace CleanArchitectureNetCore.Domain.ValueObjects;

public class Url : ValueObject<Url>
{
    private Url(string url)
    {
        Value = url;
    }

    public string Value { get; set; }

    public static Result<Url> Create(string url)
    {
        url = (url ?? string.Empty).Trim();

        if (url.Length == 0)
            return Result.Failure<Url>("Url should not be empty");

        return Result.Success(new Url(url));
    }

    protected override bool EqualsCore(Url other)
    {
        return Value.Equals(other.Value, StringComparison.InvariantCultureIgnoreCase);
    }

    protected override int GetHashCodeCore()
    {
        return Value.GetHashCode();
    }

    public static implicit operator string(Url url)
    {
        return url.Value;
    }

    public static explicit operator Url(string url)
    {
        return Create(url).Value;
    }
}