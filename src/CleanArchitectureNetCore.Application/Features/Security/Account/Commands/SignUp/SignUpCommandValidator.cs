using CleanArchitectureNetCore.Application.Contracts.Identity;
using CleanArchitectureNetCore.Domain.ValueObjects;
using CSharpFunctionalExtensions;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CleanArchitectureNetCore.Application.Features.Security.Account.Commands.SignUp;

public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    private readonly IAuthenticationService _authenticationService;

    public SignUpCommandValidator(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;

        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty).WithMessage("{PropertyName} cannot be empty")
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull();

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(250).WithMessage("{PropertyName} must not exceed 250 characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(250).WithMessage("{PropertyName} must not exceed 250 characters");

        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(256).WithMessage("{PropertyName} must not exceed 256 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(256).WithMessage("{PropertyName} must not exceed 256 characters")
            .Matches(@"^(.+)@(.+)$").WithMessage("{PropertyName} is invalid");

        RuleFor(x => x.UserName)
            .MustAsync(NotExistsUserName)
            .WithMessage("{PropertyName} already exists");

        RuleFor(x => x.Email)
            .MustAsync(NotExistsEmail)
            .WithMessage("{PropertyName} already exists");

        RuleFor(x => x.Password)
            .MustAsync(IsPasswordValid)
            .WithMessage("{PropertyName} is invalid");
    }

    private async Task<bool> NotExistsUserName(string userName, CancellationToken token)
    {
        return !await _authenticationService.ExistsUserNameAsync(userName);
    }

    private async Task<bool> NotExistsEmail(string email, CancellationToken token)
    {
        return !await _authenticationService.ExistsEmailAsync(email);
    }

    private async Task<bool> IsPasswordValid(string password, CancellationToken token)
    {
        return await _authenticationService.PasswordValidAsync(password);
    }
}