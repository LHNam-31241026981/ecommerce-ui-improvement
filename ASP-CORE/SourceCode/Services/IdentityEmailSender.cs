using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Filmora.Services;

public sealed class IdentityEmailSender(IConfiguration configuration, IWebHostEnvironment environment) : IEmailSender
{
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var gmail = configuration["Email:Address"];
        var appPassword = configuration["Email:AppPassword"];
        if (string.IsNullOrWhiteSpace(gmail) || string.IsNullOrWhiteSpace(appPassword))
        {
            if (environment.IsDevelopment())
                return;

            throw new InvalidOperationException("Hãy cấu hình dịch vụ email để gửi liên kết xác thực.");
        }

        using var message = new MailMessage(gmail, email, subject, htmlMessage) { IsBodyHtml = true };
        using var client = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new NetworkCredential(gmail, appPassword),
            EnableSsl = true
        };
        await client.SendMailAsync(message);
    }
}
