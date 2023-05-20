using CSharpFunctionalExtensions;

namespace CleanArchitectureNetCore.Domain.ValueObjects;

public class UUID : ValueObject<UUID>
{
    private UUID(Guid uuid)
    {
        Value = uuid;
    }

    public Guid Value { get; set; }

    public static Result<UUID> Create(Guid uuid)
    {
        if (uuid == Guid.Empty)
            return Result.Failure<UUID>("Guid should not be empty");

        return Result.Success(new UUID(uuid));
    }

    protected override bool EqualsCore(UUID other)
    {
        return Equals(other.Value);
    }

    protected override int GetHashCodeCore()
    {
        return Value.GetHashCode();
    }

    public static implicit operator Guid(UUID uuid)
    {
        return uuid.Value;
    }

    public static explicit operator UUID(Guid uuid)
    {
        return Create(uuid).Value;
    }
}