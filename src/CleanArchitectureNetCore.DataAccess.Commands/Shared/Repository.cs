using CleanArchitectureNetCore.Application.Contracts.Persistence;
using CleanArchitectureNetCore.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureNetCore.DataAccess.Commands.Shared;

public class Repository<T> : IRepository<T> where T : class, IEntity<Guid>
{
    protected readonly IDatabaseContext _database;

    public Repository(IDatabaseContext database)
    {
        _database = database;
    }

    public void Update(T entity)
    {
        _database.Set<T>().Entry(entity).State = EntityState.Modified;
    }

    public void Add(T entity)
    {
        _database.Set<T>().Add(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
        _database.Set<T>().AddRange(entities);
    }

    public void Delete(T entity)
    {
        _database.Set<T>().Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        _database.Set<T>().RemoveRange(entities);
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _database.Set<T>().SingleAsync(s => s.Id == id);
    }

    public async Task<bool> ExistByIdAsync(Guid id)
    {
        return await _database.Set<T>().AnyAsync(a => a.Id == id);
    }
}