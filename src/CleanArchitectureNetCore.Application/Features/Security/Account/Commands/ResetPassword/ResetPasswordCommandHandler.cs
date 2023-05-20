using CleanArchitectureNetCore.Application.Common;
using CleanArchitectureNetCore.Application.Contracts.Identity;
using CSharpFunctionalExtensions;
using MediatR;

namespace CleanArchitectureNetCore.Application.Features.Security.Account.Commands.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result>
{
    private readonly IAuthenticationService _authenticationService;

    public ResetPasswordCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var validationResult =
            await new ResetPasswordCommandValidator(_authenticationService).ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Failure(validationResult.ErrorMessages());

        await _authenticationService.SendConfirmationEmailForPasswordResetAsync(request.Email);

        return Result.Success();
    }
}