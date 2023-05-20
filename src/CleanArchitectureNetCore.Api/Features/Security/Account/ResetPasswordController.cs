using CleanArchitectureNetCore.Api.Common;
using CleanArchitectureNetCore.Application.Features.Security.Account.Commands.ResetPassword;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureNetCore.Api.Features.Security.Account;


[Route("api/security/[controller]")]
[ApiController]
public class ResetPasswordController : BaseController
{
    private readonly IMediator _mediator;

    public ResetPasswordController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsFailure)
            return Error(result.Error);

        return Ok();
    }
}