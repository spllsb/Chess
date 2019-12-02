using System.Threading.Tasks;

namespace Chess.Infrastructure.Commands
{
    
    public interface ICommandDispatcher
    {
        Task DispatchAsync<T>(T command) where T: ICommand;
    }
}