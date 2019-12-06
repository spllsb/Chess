
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Infrastructure.DTO;

namespace Chess.Infrastructure.Services
{
    public interface ITournamentService : IService
    {
        Task<TournamentDto> GetAsync(Guid tournamentId);
        Task<TournamentDto> GetAsync(string name);
        Task<IEnumerable<TournamentDetailsDto>> BrowseAsync();
        Task CreateAsync(string name,int maxPlayers);
    }
}