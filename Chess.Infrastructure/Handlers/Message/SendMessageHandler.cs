using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Commands.Message;
using Chess.Infrastructure.Services;

namespace Chess.Infrastructure.Handlers.Message
{
    public class SendMessageHandler : ICommandHandler<SendMessage>
    {
        private readonly IEmailSender _emailSender;
        public SendMessageHandler(IEmailSender emailSender
                                )
        {
            _emailSender = emailSender;
        }
        public async Task HandleAsync(SendMessage command)
        {
            foreach (var item in command.SendToList)
            {
                await _emailSender.SendEmailAsync(item, command.Title,command.Content);
            }
        }
    }
}