namespace CleanArchitectureNetCore.Application.Features.Security.Account.Commands.Login;

public record LoginCommandRespond(string UserName, string BearerToken);