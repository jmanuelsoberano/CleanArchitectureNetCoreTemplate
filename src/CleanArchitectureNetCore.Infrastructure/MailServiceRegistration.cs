using CleanArchitectureNetCore.Application.Contracts.Mail;
using CleanArchitectureNetCore.Infrastructure.Mail.Models;
using CleanArchitectureNetCore.Infrastructure.Mail.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitectureNetCore.Infrastructure;

public static class MailServiceRegistration
{
    public static IServiceCollection AddMailServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddScoped<IEmailService, EmailService>();

        return services;
    }
}
