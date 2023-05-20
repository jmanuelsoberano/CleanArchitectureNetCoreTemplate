using CleanArchitectureNetCore.Application.Common;
using CleanArchitectureNetCore.Application.Contracts.Identity;
using CSharpFunctionalExtensions;
using MediatR;

namespace CleanArchitectureNetCore.Application.Features.Security.Account.Commands.ConfirmResetPassword;

public class ConfirmResetPasswordCommandHandler : IRequestHandler<ConfirmResetPasswordCommand, Result>
{
    private readonly IAuthenticationService _authenticationService;

    public ConfirmResetPasswordCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<Result> Handle(ConfirmResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var validationResult =
            await new ConfirmResetPasswordCommandValidator(_authenticationService)
                .ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Failure(validationResult.ErrorMessages());

        await _authenticationService.ResetPasswordAsync(request.Email, request.Code, request.NewPassword);

        return Result.Success();
    }
}