
using System.Threading.Tasks;
using Chess.Infrastructure.DTO;

namespace Chess.Infrastructure.Services
{
    public interface IChessGameService : IService
    {
        Task AddToWaitingList(string userId);
        Task<PlayerDto> GetPlayerFromWaitingList();
    }

}