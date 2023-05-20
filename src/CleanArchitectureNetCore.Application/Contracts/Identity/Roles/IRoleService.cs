namespace CleanArchitectureNetCore.Application.Contracts.Identity.Roles;

public interface IRoleService
{
    IQueryable<Role> Get();
    Task<RoleWithClaims> GetAsync(Guid id);
    Task<bool> ExistAsync(Guid id);
    Task<bool> ExistAsync(string name);
    Task CreateAsync(RoleWithClaims request);
    Task DeleteAsync(Guid id);
    Task UpdateAsync(RoleWithClaims request);
}