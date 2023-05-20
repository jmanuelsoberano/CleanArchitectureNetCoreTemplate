using CSharpFunctionalExtensions;
using MediatR;

namespace CleanArchitectureNetCore.Application.Features.Security.Account.Commands.SignUp;

public record SignUpCommand
    (Guid Id, string UserName, string Email, string Password, string FirstName, string LastName) : IRequest<Result>;