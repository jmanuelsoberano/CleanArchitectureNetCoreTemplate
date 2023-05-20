using CleanArchitectureNetCore.Api.Common;
using CleanArchitectureNetCore.Application.Features.Security.Account.Commands.ConfirmEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureNetCore.Api.Features.Security.Account;

[Route("api/security/[controller]")]
[ApiController]
public class ConfirmEmailController : BaseController
{
    private readonly IMediator _mediator;

    public ConfirmEmailController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmEmail(ConfirmEmailCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsFailure)
            return Error(result.Error);

        return Ok();
    }
}