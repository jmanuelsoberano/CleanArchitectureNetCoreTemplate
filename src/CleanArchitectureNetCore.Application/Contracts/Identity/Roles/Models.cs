namespace CleanArchitectureNetCore.Application.Contracts.Identity.Roles;

public record Role(Guid Id, string Name);

public record RoleWithClaims(Guid Id, string Name, List<string> Claims);
