using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Chess.Infrastructure.Services
{
    public interface IManageTournamentService : IService
    {
        Task AddPlayerToTournament(Guid tournamentId, string playerName);
        Task<IEnumerable<Guid>> GetPlayersFromTournament(Guid tournamentId);
        
    }
}