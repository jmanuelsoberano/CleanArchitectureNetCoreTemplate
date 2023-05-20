using CleanArchitectureNetCore.Api.Common;
using CleanArchitectureNetCore.Application.Features.Security.Account.Commands.ConfirmResetPassword;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureNetCore.Api.Features.Security.Account;


[Route("api/security/[controller]")]
[ApiController]
public class ConfirmResetPasswordController : BaseController
{
    private readonly IMediator _mediator;

    public ConfirmResetPasswordController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmEmail(ConfirmResetPasswordCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsFailure)
            return Error(result.Error);

        return Ok();
    }
}