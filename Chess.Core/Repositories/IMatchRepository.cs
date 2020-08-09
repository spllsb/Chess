

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Core.Domain;

namespace Chess.Core.Repositories
{
    public interface IMatchRepository : IRepository
    {
        Task<Match> GetAsync(Guid id);
        Task<IEnumerable<Match>> GetMatchByPlayerAsync(Guid playerId);
        Task<IEnumerable<Match>> GetMatchByTournamentAsync(Guid tournamentId);

        
        Task<IEnumerable<Match>> GetAllAsync();
        Task AddAsync(Match match);
    }
}