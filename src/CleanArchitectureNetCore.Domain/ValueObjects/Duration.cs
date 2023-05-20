using CSharpFunctionalExtensions;

namespace CleanArchitectureNetCore.Domain.ValueObjects;

public class Duration : ValueObject<Duration>
{
    private Duration(TimeSpan duration)
    {
        Value = duration;
    }

    public TimeSpan Value { get; set; }

    public static Result<Duration> Create(TimeSpan duration)
    {
        if (duration == TimeSpan.Zero)
            return Result.Failure<Duration>("Duration must be greater than zero");

        return Result.Success(new Duration(duration));
    }

    protected override bool EqualsCore(Duration other)
    {
        return Value.Equals(other.Value);
    }

    protected override int GetHashCodeCore()
    {
        return Value.GetHashCode();
    }

    public static implicit operator TimeSpan(Duration duration)
    {
        return duration.Value;
    }

    public static explicit operator Duration(TimeSpan duration)
    {
        return Create(duration).Value;
    }
}