using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Commands.Users;

namespace Chess.Infrastructure.Handlers.Users
{
    public class ChangeUserPasswordHandler : ICommandHandler<SetPassword>
    {
        public async Task HandleAsync(SetPassword command)
        {
            await Task.CompletedTask;
        }
    }
}