using CleanArchitectureNetCore.Application.Common;
using CleanArchitectureNetCore.Application.Contracts.Identity;
using CSharpFunctionalExtensions;
using MediatR;

namespace CleanArchitectureNetCore.Application.Features.Security.Account.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginCommandRespond>>
{
    private readonly IAuthenticationService _authenticationService;

    public LoginCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<Result<LoginCommandRespond>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var validationResult =
            await new LoginCommandValidator(_authenticationService).ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Failure<LoginCommandRespond>(validationResult.ErrorMessages());

        var loginRespond = await _authenticationService.LoginAsync(request.UserName, request.Password);

        return Result.Success(new LoginCommandRespond(loginRespond.UserName, loginRespond.Token));
    }
}