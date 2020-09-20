
using System;
using System.Threading.Tasks;
using Chess.Core.Domain.Enum;
using Chess.Infrastructure.DTO;

namespace Chess.Infrastructure.Services
{
    public interface IChessGameService : IService
    {
        Task<int> GetWaitingPlayerListCount();
        Task AddToWaitingList(PlayerInRoom playerInRoom);
        Task RemoveFromWaitingList(PlayerInRoom player);
        Task<PlayerInRoom> GetPlayerFromWaitingList(int gameDuration, int ratingELO, SearchingGameTypEnum searchingGameTyp);
        Task<PlayerInRoom> GetPlayerFromWaitingList(string connectionId);
        Task<string> GetRandomColorPiece();
        Task RemoveCurrentPlayerFromWaitingList(string connectionId);
        int CountOpponent(int gameDuration, int ratingELO, SearchingGameTypEnum searchingGameTyp);

        Task InitMatch(PlayerInRoom playerOne, PlayerInRoom playerTwo, string gameId); 
        Task RemoveMatch(string roomId);
        Task<ChessMatch> GetChessMatch (string roomId);
        Task<int> GetCurrentMatchCount();
        Task SaveChessMatchOnDatabase(string roomId);
        Task<string> GetRoomId(string connectionId); 

        Task RemoveCurrentPlayer(PlayerInRoom player);
        Task<PlayerInRoom> GetCurrentPlayer(string connectionId);
        Task<PlayerInRoom> GetCurrentPlayer(Guid playerId);
        Task<int> GetCurrentPlayerListCount();
        Task AddPlayerToCurrentPlayerList(PlayerInRoom playerInRoom);

    }

}