using System.Threading.Tasks;

namespace Chess.Infrastructure.Commands
{
    public interface ICommandHandler<T> where T: ICommand
    {
        Task HandleAsync(T command); 
    }
}