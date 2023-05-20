using CleanArchitectureNetCore.Application.Contracts;

namespace CleanArchitectureNetCore.Api.Services;

public class LoggedInUserService : ILoggedInUserService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
    {
        _contextAccessor = httpContextAccessor;
        //UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public string UserId =>
        //return _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        "useridFAKE";
}