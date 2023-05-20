using CSharpFunctionalExtensions;

namespace CleanArchitectureNetCore.Application.Contracts.Identity;

public interface IAuthenticationService
{
    Task<LoginResponse> LoginAsync(string UserName, string Password);
    Task<Result<SignUpResponse>> SingUpAsync(SignUpRequest request);
    Task SendConfirmationEmailForPasswordResetAsync(string email);
    Task ResetPasswordAsync(string email, string code, string newPassword);
    Task<bool> UserWithPasswordFoundAsync(string UserName, string Password);
    Task<bool> ExistsUserNameAsync(string userName);
    Task<bool> ExistsEmailAsync(string email);
    Task<bool> PasswordValidAsync(string password);
    Task ConfirmEmailAsync(string email, string code);
}