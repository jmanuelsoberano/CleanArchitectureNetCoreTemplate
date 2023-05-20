namespace CleanArchitectureNetCore.Domain.Common;

public interface IAggregateRoot
{
    public Guid Id { get; set; }
}