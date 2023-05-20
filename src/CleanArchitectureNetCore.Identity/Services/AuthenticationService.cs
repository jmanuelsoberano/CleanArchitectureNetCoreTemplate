using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using CleanArchitectureNetCore.Application.Contracts.Identity;
using CleanArchitectureNetCore.Application.Contracts.Mail;
using CleanArchitectureNetCore.Identity.Models;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitectureNetCore.Identity.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;
    private readonly JwtSettings _jwtSettings;

    public AuthenticationService(UserManager<ApplicationUser> userManager,
        IOptions<JwtSettings> jwtSettings,
        SignInManager<ApplicationUser> signInManager,
        IEmailService emailService,
        IConfiguration configuration
    )
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
        _signInManager = signInManager;
        _configuration = configuration;
        _emailService = emailService;
    }

    public async Task<LoginResponse> LoginAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);

        var result = await _signInManager.PasswordSignInAsync(user.UserName, password, false, false);

        var jwtSecurityToken = await GenerateTokenAsync(user);

        var response = new LoginResponse(user.Id, user.UserName, user.Email,
            new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken));

        return response;
    }

    public async Task<Result<SignUpResponse>> SingUpAsync(SignUpRequest request)
    {
        var user = new ApplicationUser
        {
            Id = request.Id,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            EmailConfirmed = false
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            await SendConfirmationEmailAsync(user);
            return new SignUpResponse(user.Id);
        }

        return Result.Failure<SignUpResponse>(string.Join(",", result.Errors.Select(s => s.Description)));
    }

    public async Task SendConfirmationEmailForPasswordResetAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var tokenEncode = Base64UrlEncoder.Encode(Encoding.UTF8.GetBytes(token));
        var urlConfirmResetPassword =
            $"{_configuration.GetSection("Urls").GetSection("FrontEnd").Value}{_configuration.GetSection("Urls").GetSection("ConfirmResetPassword").Value}?email={user.Email}&code={tokenEncode}";
        var sendEmail = await _emailService.SendEmailAsync(user.Email, "Confirm your reset password",
            $"Please confirm your reset password by <a href='{HtmlEncoder.Default.Encode(urlConfirmResetPassword)}'>clicking here</a>.");
    }

    public async Task ResetPasswordAsync(string email, string code, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var tokenDecode = Base64UrlEncoder.Decode(code);
        var result = await _userManager.ResetPasswordAsync(user, tokenDecode, newPassword);
        if (result.Errors.Any())
        {
            var errors = Result.Combine(result.Errors.Select(s => Result.Failure(s.Description))).Error;
            throw new Exception($"{errors}");
        }
    }

    private async Task SendConfirmationEmailAsync(ApplicationUser user)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var tokenEncode = Base64UrlEncoder.Encode(Encoding.UTF8.GetBytes(token));
        var urlConfirmMail =
            $"{_configuration.GetSection("Urls").GetSection("FrontEnd").Value}{_configuration.GetSection("Urls").GetSection("ConfirmEmail").Value}?email={user.Email}&code={tokenEncode}";
        var sendEmail = await _emailService.SendEmailAsync(user.Email, "Confirm your email",
            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(urlConfirmMail)}'>clicking here</a>.");
    }

    public async Task<bool> UserWithPasswordFoundAsync(string userName, string password)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user != null)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            return result.Succeeded;
        }

        return false;
    }

    public async Task<bool> ExistsUserNameAsync(string userName)
    {
        return await _userManager.Users.AnyAsync(a => a.NormalizedUserName == userName.ToUpper());
    }

    public async Task<bool> ExistsEmailAsync(string email)
    {
        return await _userManager.Users.AnyAsync(a => a.NormalizedEmail == email.ToUpper());
    }

    public async Task<bool> PasswordValidAsync(string password)
    {
        var passwordValidator = new PasswordValidator<ApplicationUser>();
        var result = await passwordValidator.ValidateAsync(_userManager, null!, password);

        return result.Succeeded;
    }

    public async Task ConfirmEmailAsync(string email, string code)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var tokenDecode = Base64UrlEncoder.Decode(code);
        var result = await _userManager.ConfirmEmailAsync(user, tokenDecode);
        if (result.Errors.Any())
        {
            var errors = Result.Combine(result.Errors.Select(s => Result.Failure(s.Description))).Error;
            throw new Exception($"{errors}");
        }
    }

    private async Task<JwtSecurityToken> GenerateTokenAsync(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();

        for (var i = 0; i < roles.Count; i++) roleClaims.Add(new Claim("roles", roles[i]));

        var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }
}