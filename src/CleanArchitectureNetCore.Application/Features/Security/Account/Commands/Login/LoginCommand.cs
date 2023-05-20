using CSharpFunctionalExtensions;
using MediatR;

namespace CleanArchitectureNetCore.Application.Features.Security.Account.Commands.Login;

public record LoginCommand(string UserName, string Password) : IRequest<Result<LoginCommandRespond>>;