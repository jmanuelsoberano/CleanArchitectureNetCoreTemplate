namespace CleanArchitectureNetCore.Application.Contracts.Persistence;

public interface IUnitOfWork
{
    Task SaveAsync(CancellationToken cancellationToken = new());
}