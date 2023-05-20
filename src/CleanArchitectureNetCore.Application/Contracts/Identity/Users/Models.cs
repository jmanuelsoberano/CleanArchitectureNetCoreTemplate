namespace CleanArchitectureNetCore.Application.Contracts.Identity.Users;

public record UserWithRoles(Guid Id, string UserName, string Email, string PhoneNumber, string FirstName,
    string LastName,
    Dictionary<string, string> Roles);

public record User(string Id, string UserName, string Email, string PhoneNumber, string FirstName, string LastName);
