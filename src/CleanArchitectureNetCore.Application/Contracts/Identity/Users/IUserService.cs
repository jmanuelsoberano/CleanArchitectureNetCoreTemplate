namespace CleanArchitectureNetCore.Application.Contracts.Identity.Users;

public interface IUserService
{
    IQueryable<User> Get();
    Task<UserWithRoles> GetAsync(Guid id);
    Task<bool> ExistAsync(Guid id);
    Task<bool> ExistAsync(string userName);
    Task<bool> ExistEmailAsync(string email);
    Task CreateAsync(UserWithRoles request);
    Task DeleteAsync(Guid id);
    Task UpdateAsync(UserWithRoles request);
    Task<bool> ExistRolesAsync(List<string> roles);
    Task<bool> ExistUserNameAsync(string userName);
    Task<bool> ExistEmailDuplicateAsync(Guid userId, string email);
    string GetCurrentUserName();
}