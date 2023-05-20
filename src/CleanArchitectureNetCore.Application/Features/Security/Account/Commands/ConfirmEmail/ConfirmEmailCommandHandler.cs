using CleanArchitectureNetCore.Application.Common;
using CleanArchitectureNetCore.Application.Contracts.Identity;
using CSharpFunctionalExtensions;
using MediatR;

namespace CleanArchitectureNetCore.Application.Features.Security.Account.Commands.ConfirmEmail;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Result>
{
    private readonly IAuthenticationService _authenticationService;

    public ConfirmEmailCommandHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<Result> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var validationResult =
            await new ConfirmEmailCommandValidator(_authenticationService).ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Failure(validationResult.ErrorMessages());

        await _authenticationService.ConfirmEmailAsync(request.Email, request.Code);

        return Result.Success();
    }
}