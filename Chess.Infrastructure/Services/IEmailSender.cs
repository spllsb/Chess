using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chess.Infrastructure.Services;

namespace Chess.Infrastructure.Services
{
    public interface IEmailSender : IService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
