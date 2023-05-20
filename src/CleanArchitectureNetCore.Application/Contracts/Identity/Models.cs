namespace CleanArchitectureNetCore.Application.Contracts.Identity;

public record LoginResponse(string Id, string UserName, string Email, string Token);

public record SignUpRequest(string Id, string UserName, string Email, string Password, string FirstName,
    string LastName);

public record SignUpResponse(string UserId);
