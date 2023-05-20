using System.Net;
using CleanArchitectureNetCore.Application.Contracts.Mail;
using CleanArchitectureNetCore.Infrastructure.Mail.Models;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace CleanArchitectureNetCore.Infrastructure.Mail.Services;

public class EmailService : IEmailService
{
    public EmailSettings _emailSettings { get; }

    public EmailService(IOptions<EmailSettings> mailSettings)
    {
        _emailSettings = mailSettings.Value;
    }

    public async Task<bool> SendEmailAsync(string to, string subject, string body)
    {
        var client = new SendGridClient(_emailSettings.ApiKey);

        var emailTo = new EmailAddress(to);
        var emailSubject = subject;
        var emailBody = body;

        var from = new EmailAddress
        {
            Email = _emailSettings.FromAddress,
            Name = _emailSettings.FromName
        };

        var sendGridMessage = MailHelper.CreateSingleEmail(from, emailTo, emailSubject, emailBody, emailBody);
        var response = await client.SendEmailAsync(sendGridMessage);

        if (response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.OK)
            return true;

        return false;
    }
}