using CleanArchitectureNetCore.Application.Contracts.Persistence;

namespace CleanArchitectureNetCore.DataAccess.Commands.Shared;

public class UnitOfWork : IUnitOfWork
{
    private readonly IDatabaseContext _database;

    public UnitOfWork(IDatabaseContext database)
    {
        _database = database;
    }

    public async Task SaveAsync(CancellationToken cancellationToken = new())
    {
        await _database.SaveAsync(cancellationToken);
    }
}