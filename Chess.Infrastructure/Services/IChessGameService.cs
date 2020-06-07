
using System.Threading.Tasks;
using Chess.Infrastructure.DTO;

namespace Chess.Infrastructure.Services
{
    public interface IChessGameService : IService
    {
        Task AddToWaitingList(string userId, string connedtionId);
        Task RemoveFromWaitingList(PlayerInRoom player);
        Task<PlayerInRoom> GetPlayerFromWaitingList();
        int CountOpponent();

    }

}