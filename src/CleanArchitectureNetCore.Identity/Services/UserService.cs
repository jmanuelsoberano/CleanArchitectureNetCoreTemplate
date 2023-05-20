using System.Security.Claims;
using CleanArchitectureNetCore.Application.Contracts.Identity.Users;
using CleanArchitectureNetCore.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CleanArchitectureNetCore.Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly JwtSettings _jwtSettings;

    public UserService(UserManager<ApplicationUser> userManager,
        IOptions<JwtSettings> jwtSettings,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public IQueryable<User> Get()
    {
        return _userManager.Users.Select(s =>
            new User(s.Id, s.UserName, s.Email, s.PhoneNumber, s.FirstName, s.LastName));
    }

    public async Task<UserWithRoles> GetAsync(Guid id)
    {
        var user = await GetAsync(id.ToString());
        var rolesName = await _userManager.GetRolesAsync(user);


        var roles = await _roleManager.Roles
            .Where(w => rolesName.Contains(w.Name))
            .Select(s => new KeyValuePair<string, string>(s.Id, s.Name))
            .ToDictionaryAsync(x => x.Key, x => x.Value);

        return new UserWithRoles(Guid.Parse(user.Id), user.UserName, user.Email, user.PhoneNumber, user.FirstName,
            user.LastName, roles);
    }

    private async Task<ApplicationUser> GetAsync(string id)
    {
        return await _userManager.Users.Where(w => w.Id == id).SingleAsync();
    }

    public string GetCurrentUserName()
    {
        return _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
    }

    public async Task<bool> ExistAsync(Guid id)
    {
        return await _userManager.Users.AnyAsync(a => a.Id == id.ToString());
    }

    public async Task<bool> ExistAsync(string userName)
    {
        return await _userManager.Users.AnyAsync(a => a.NormalizedUserName == userName.ToUpper());
    }

    public async Task<bool> ExistEmailAsync(string email)
    {
        return await _userManager.Users.AnyAsync(a => a.NormalizedEmail == email.ToUpper());
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await GetAsync(id.ToString());
        var result = await _userManager.DeleteAsync(user);

        if (result.Errors.Any()) throw new Exception(result.Errors.ToString());
    }

    public async Task CreateAsync(UserWithRoles request)
    {
        var user = new ApplicationUser
        {
            Id = request.Id.ToString(),
            UserName = request.UserName,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber
        };
        var userResult = await _userManager.CreateAsync(user);

        if (userResult.Errors.Any()) throw new Exception(userResult.Errors.ToString());

        await _userManager.AddToRolesAsync(user, request.Roles.Values);
    }

    public async Task UpdateAsync(UserWithRoles request)
    {
        var user = await _userManager.Users.Where(w => w.Id == request.Id.ToString()).SingleAsync();

        var roles = await _userManager.GetRolesAsync(user);
        var rolesRemoveResult = await _userManager.RemoveFromRolesAsync(user, roles);
        if (rolesRemoveResult.Errors.Any()) throw new Exception(rolesRemoveResult.Errors.ToString());

        var rolesAddResult = await _userManager.AddToRolesAsync(user, request.Roles.Values);
        if (rolesAddResult.Errors.Any()) throw new Exception(rolesAddResult.Errors.ToString());

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.PhoneNumber = request.PhoneNumber;
        user.Email = request.Email;

        await _userManager.UpdateAsync(user);
    }

    public async Task<bool> ExistRolesAsync(List<string> roles)
    {
        var unduplicated = roles.Select(s => s.ToUpper()).Distinct();
        return await _roleManager.Roles.CountAsync(w => unduplicated.Contains(w.NormalizedName)) == roles.Count();
    }

    public async Task<bool> ExistUserNameAsync(string userName)
    {
        return await _userManager.Users.AnyAsync(a => a.NormalizedUserName == userName.ToUpper());
    }

    public async Task<bool> ExistEmailDuplicateAsync(Guid userId, string email)
    {
        return await _userManager.Users.CountAsync(a => a.Id != userId.ToString() && a.NormalizedEmail == email) > 0;
    }
}