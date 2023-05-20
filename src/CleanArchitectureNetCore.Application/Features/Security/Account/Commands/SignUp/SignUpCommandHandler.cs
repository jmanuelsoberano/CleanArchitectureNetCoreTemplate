using CleanArchitectureNetCore.Application.Common;
using CleanArchitectureNetCore.Application.Contracts.Identity;
using CSharpFunctionalExtensions;
using MediatR;

namespace CleanArchitectureNetCore.Application.Features.Security.Account.Commands.SignUp;

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, Result>
{
    private readonly IAuthenticationService _authenticationService;

    public SignUpCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<Result> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var validationResult =
            await new SignUpCommandValidator(_authenticationService).ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Failure(validationResult.ErrorMessages());

        var signupRequest = new SignUpRequest(request.Id.ToString(), request.UserName, request.Email, request.Password,
            request.FirstName, request.LastName);
        var resultSingUpAsync = await _authenticationService.SingUpAsync(signupRequest);

        return resultSingUpAsync;
    }
}