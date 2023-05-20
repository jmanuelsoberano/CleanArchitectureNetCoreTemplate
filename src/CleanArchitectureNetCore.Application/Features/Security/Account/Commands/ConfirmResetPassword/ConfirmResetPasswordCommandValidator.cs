using CleanArchitectureNetCore.Application.Contracts.Identity;
using FluentValidation;

namespace CleanArchitectureNetCore.Application.Features.Security.Account.Commands.ConfirmResetPassword;

public class ConfirmResetPasswordCommandValidator : AbstractValidator<ConfirmResetPasswordCommand>
{
    private readonly IAuthenticationService _authenticationService;

    public ConfirmResetPasswordCommandValidator(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;

        RuleFor(x => x.Email)
            .MustAsync(ExistsEmailAsync)
            .WithMessage("{PropertyName} does not exist");
    }

    private async Task<bool> ExistsEmailAsync(string email, CancellationToken token)
    {
        return await _authenticationService.ExistsEmailAsync(email);
    }
}