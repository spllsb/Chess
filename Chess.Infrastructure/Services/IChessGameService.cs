
using System.Threading.Tasks;
using Chess.Infrastructure.DTO;

namespace Chess.Infrastructure.Services
{
    public interface IChessGameService : IService
    {
        Task AddToWaitingList(PlayerInRoom playerInRoom);
        Task RemoveFromWaitingList(PlayerInRoom player);
        Task<PlayerInRoom> GetPlayerFromWaitingList(int gameDuration);
        int CountOpponent(int gameDuration);

    }

}