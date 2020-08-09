
using System.Threading.Tasks;
using Chess.Infrastructure.DTO;

namespace Chess.Infrastructure.Services
{
    public interface IChessGameService : IService
    {
        Task AddToWaitingList(PlayerInRoom playerInRoom);
        Task RemoveFromWaitingList(PlayerInRoom player);
        Task<PlayerInRoom> GetPlayerFromWaitingList(int gameDuration);
        Task RemoveCurrentPlayerFromWaitingList(string connectionId);
        int CountOpponent(int gameDuration);

        Task InitMatch(PlayerInRoom playerOne, PlayerInRoom playerTwo, string gameId); 
        Task<string> GetRoomId(string connectionId); 

    }

}