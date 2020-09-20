using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Infrastructure.DTO;

namespace Chess.Infrastructure.Services
{
    public interface IMatchService : IService
    {

        // TODO 
        //wyswietlam liste meczy dla danego turnieju
        //wyswietlamy liste mezy dla uzytkownika
        //wyswietlamy liste meczy dla klubu
        Task<MatchDto> GetAsync(Guid matchId);
        Task<IEnumerable<MatchDto>> GetByPlayerAsync(Guid playerId);
        Task<IEnumerable<MatchDto>> GetByTournamentAsync(Guid tournamentId);

        Task<IEnumerable<MatchDto>> BrowseAsync();

        Task <IEnumerable<MatchDto>> GetMatchStartingNowAsync();

        Task CreateMatch(Guid firstPlayerId, Guid secondPlayerId, DateTime begin_at, int duringTime);

        Task <IEnumerable<MatchDto>> PagedList(MatchParameters parameters);
    }
}