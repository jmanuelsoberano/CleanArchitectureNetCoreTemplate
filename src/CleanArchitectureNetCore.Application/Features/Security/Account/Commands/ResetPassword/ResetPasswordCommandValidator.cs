using CleanArchitectureNetCore.Application.Contracts.Identity;
using CleanArchitectureNetCore.Application.Features.Security.Account.Commands.ConfirmEmail;
using FluentValidation;

namespace CleanArchitectureNetCore.Application.Features.Security.Account.Commands.ResetPassword;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    private readonly IAuthenticationService _authenticationService;

    public ResetPasswordCommandValidator(IAuthenticationService authenticationService)
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