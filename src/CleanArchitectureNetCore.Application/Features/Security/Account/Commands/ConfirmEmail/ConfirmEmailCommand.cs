using CSharpFunctionalExtensions;
using MediatR;

namespace CleanArchitectureNetCore.Application.Features.Security.Account.Commands.ConfirmEmail;

public record ConfirmEmailCommand(string Email, string Code) : IRequest<Result>;