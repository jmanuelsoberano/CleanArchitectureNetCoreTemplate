using CleanArchitectureNetCore.Application.Contracts.Identity;
using FluentValidation;

namespace CleanArchitectureNetCore.Application.Features.Security.Account.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    private readonly IAuthenticationService _authenticationService;

    public LoginCommandValidator(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;

        RuleFor(x => x)
            .MustAsync(ExistsUserWithPassword)
            .WithMessage("Invalid UserName/Password");
    }

    public async Task<bool> ExistsUserWithPassword(LoginCommand command, CancellationToken token)
    {
        return await _authenticationService.UserWithPasswordFoundAsync(command.UserName, command.Password);
    }
}