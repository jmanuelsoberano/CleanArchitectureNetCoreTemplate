using System.Security.Claims;
using CleanArchitectureNetCore.Application.Contracts.Identity.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureNetCore.Identity.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleService(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public IQueryable<Role> Get()
    {
        return _roleManager.Roles.Select(s => new Role(Guid.Parse(s.Id), s.Name));
    }

    public async Task<RoleWithClaims> GetAsync(Guid id)
    {
        var role = await GetAsync(id.ToString());

        var claims = await _roleManager.GetClaimsAsync(role);
        var claimsString = claims.Select(s => s.Type).ToList();

        return new RoleWithClaims(Guid.Parse(role.Id), role.Name, claimsString);
    }

    private async Task<IdentityRole> GetAsync(string id)
    {
        var role = await _roleManager.Roles.SingleAsync(w => w.Id == id);

        return role;
    }

    public async Task<bool> ExistAsync(Guid id)
    {
        return await _roleManager.Roles.AnyAsync(a => a.Id == id.ToString());
    }

    public async Task<bool> ExistAsync(string name)
    {
        return await _roleManager.Roles.AnyAsync(a => a.NormalizedName == name.ToUpper());
    }

    public async Task DeleteAsync(Guid id)
    {
        var role = await GetAsync(id.ToString());
        var result = await _roleManager.DeleteAsync(role);

        if (result.Errors.Any()) throw new Exception(result.Errors.ToString());
    }

    public async Task CreateAsync(RoleWithClaims request)
    {
        var role = new IdentityRole(request.Name);
        role.Id = request.Id.ToString();
        await _roleManager.CreateAsync(role);

        foreach (var claim in request.Claims)
        {
            await _roleManager.AddClaimAsync(role, new Claim(claim, "true"));
        }
    }

    public async Task UpdateAsync(RoleWithClaims request)
    {
        var role = await _roleManager.Roles.Where(w => w.Id == request.Id.ToString()).SingleAsync();
        var claims = await _roleManager.GetClaimsAsync(role);

        foreach (var claim in claims)
        {
            await _roleManager.RemoveClaimAsync(role, claim);
        }


        foreach (var claim in request.Claims)
        {
            await _roleManager.AddClaimAsync(role, new Claim(claim, "true"));
        }

        role.Name = request.Name;
        await _roleManager.UpdateAsync(role);
    }
}