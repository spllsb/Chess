
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chess.Infrastructure.DTO;

namespace Chess.Infrastructure.Services
{
    public interface ITournamentService : IService
    {
        Task<TournamentDetailsDto> GetAsync(Guid tournamentId);
        Task<TournamentDetailsDto> GetAsync(string name);
        Task<IEnumerable<TournamentDto>> BrowseAsync();
        Task <IEnumerable<TournamentDto>> PagedList(TournamentParameters parameters);
        Task CreateAsync(string name,int maxPlayers);
        Task UpdateAsync(TournamentDetailsDto tournamentDetailsDto);
    }
}