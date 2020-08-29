using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Chess.Infrastructure.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link https://go.microsoft.com/fwlink/?LinkID=532713

    [Authorize]
    public class MessageSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            try
            {
                MailMessage msg = new MailMessage();
                
                msg.From = new MailAddress("projektmvc2018@gmail.com");
                msg.To.Add(email);
                msg.Subject = subject;
                msg.Body = message;
                using (SmtpClient client = new SmtpClient())
                {
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("projektmvc2018@gmail.com", "qwopasklzxnm");
                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
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
