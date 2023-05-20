using CSharpFunctionalExtensions;

namespace CleanArchitectureNetCore.Domain.ValueObjects;

public class Position : ValueObject<Position>
{
    private Position(int position)
    {
        Value = position;
    }

    public int Value { get; set; }

    public static Result<Position> Create(int position)
    {
        if (position <= 0)
            return Result.Failure<Position>("Position must be greater than zero");

        return Result.Success(new Position(position));
    }

    protected override bool EqualsCore(Position other)
    {
        return Value.Equals(other.Value);
    }

    protected override int GetHashCodeCore()
    {
        return Value.GetHashCode();
    }

    public static implicit operator int(Position guid)
    {
        return guid.Value;
    }

    public static explicit operator Position(int position)
    {
        return Create(position).Value;
    }
}