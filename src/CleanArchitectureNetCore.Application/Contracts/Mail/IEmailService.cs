using CleanArchitectureNetCore.Domain.ValueObjects;

namespace CleanArchitectureNetCore.Application.Contracts.Mail;

public interface IEmailService
{
    Task<bool> SendEmailAsync(string to, string subject, string body);
}