namespace CleanArchitectureNetCore.Application.Contracts.Persistence;

public interface IRepository<T>
{
    void Update(T entity);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);

    Task<T> GetByIdAsync(Guid id);
    Task<bool> ExistByIdAsync(Guid id);
}