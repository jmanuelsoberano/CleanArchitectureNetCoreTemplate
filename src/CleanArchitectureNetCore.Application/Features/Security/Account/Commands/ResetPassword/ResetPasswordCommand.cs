using CSharpFunctionalExtensions;
using MediatR;

namespace CleanArchitectureNetCore.Application.Features.Security.Account.Commands.ResetPassword;

public record ResetPasswordCommand(string Email) : IRequest<Result>;