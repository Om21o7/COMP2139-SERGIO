using SendGrid.Helpers.Mail;
using SendGrid;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

public class EmailSender : IEmailSender
{

    private readonly IConfiguration _configuration;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        return ConfigSendGridAsync(email, subject, htmlMessage);
    }

    private Task ConfigSendGridAsync(string email, string subject, string htmlMessage)
    {
        var apiKey = _configuration["SendGrid:ApiKey"];
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("101414422@georgebrown.ca", "Om ");
        var to = new EmailAddress(email);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
        return client.SendEmailAsync(msg);
    }
}


