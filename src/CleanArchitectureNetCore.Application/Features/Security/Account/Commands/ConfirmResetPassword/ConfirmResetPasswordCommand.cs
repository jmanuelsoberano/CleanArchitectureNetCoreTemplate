using CSharpFunctionalExtensions;
using MediatR;

namespace CleanArchitectureNetCore.Application.Features.Security.Account.Commands.ConfirmResetPassword;

public record ConfirmResetPasswordCommand(string Email, string Code, string NewPassword) : IRequest<Result>;