using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Chess.Infrastructure.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Chess.Infrastructure.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link https://go.microsoft.com/fwlink/?LinkID=532713

    [Authorize]
    public class MessageSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public MessageSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            try
            {
                MailMessage msg = new MailMessage();
                
                msg.From = new MailAddress(_emailSettings.Login);
                msg.To.Add(email);
                msg.Subject = subject;
                msg.Body = message;
                using (SmtpClient client = new SmtpClient())
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_emailSettings.Login, _emailSettings.Password);
                    client.Host = _emailSettings.Host;
                    client.Port = _emailSettings.Port;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;

                    client.Send(msg);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Email can not send. {ex}" );
            }
            return Task.FromResult(0);
        }
    }
}
