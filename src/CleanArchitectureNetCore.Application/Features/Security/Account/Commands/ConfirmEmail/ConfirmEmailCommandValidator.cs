using CleanArchitectureNetCore.Application.Contracts.Identity;
using FluentValidation;

namespace CleanArchitectureNetCore.Application.Features.Security.Account.Commands.ConfirmEmail;

public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
{
    private readonly IAuthenticationService _authenticationService;

    public ConfirmEmailCommandValidator(IAuthenticationService authenticationService)
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