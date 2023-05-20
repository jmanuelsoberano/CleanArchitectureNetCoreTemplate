using CleanArchitectureNetCore.Api.Common;
using CleanArchitectureNetCore.Application.Features.Security.Account.Commands.SignUp;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureNetCore.Api.Features.Security.Account;

[Route("api/security/[controller]")]
[ApiController]
public class SignUpController : BaseController
{
    private readonly IMediator _mediator;

    public SignUpController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsFailure)
            return Error(result.Error);

        return Ok();
    }
}