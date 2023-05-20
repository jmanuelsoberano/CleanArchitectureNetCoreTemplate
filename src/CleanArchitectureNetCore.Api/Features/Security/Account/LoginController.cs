using CleanArchitectureNetCore.Api.Common;
using CleanArchitectureNetCore.Application.Features.Security.Account.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectureNetCore.Api.Features.Security.Account;

[Route("api/security/[controller]")]
[ApiController]
public class LoginController : BaseController
{
    private readonly IMediator _mediator;

    public LoginController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginCommand command)
    {
        var result = await _mediator.Send(command);
        if (result.IsFailure)
            return Error(result.Error);

        return Ok(result.Value);
    }
}