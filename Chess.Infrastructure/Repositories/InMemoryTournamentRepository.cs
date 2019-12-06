using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chess.Core.Domain;
using Chess.Core.Repositories;

namespace Chess.Infrastructure.Repositories
{
    public class InMemoryTournamentRepository : ITournamentRepository
    {
        private static ISet<Tournament> _tournaments = new HashSet<Tournament>
        {
            new Tournament("Kadeci",10)
        };
        
        public async Task<Tournament> GetAsync(Guid id)
            => await Task.FromResult(_tournaments.SingleOrDefault(x => x.Id == id));
        public async Task<Tournament> GetAsync(string name)
            => await Task.FromResult(_tournaments.SingleOrDefault(x => x.Name == name));
        public async Task<IEnumerable<Tournament>> GetAllAsync()
            => await Task.FromResult(_tournaments);

        public async Task AddAsync(Tournament tournament)
        { 
            _tournaments.Add(tournament);
            await Task.CompletedTask;
        }
        public Task UpdateAsync(Tournament user)
        {
            throw new NotImplementedException();
        }
        public async Task RemoveAsync(Tournament tournament)
        {
            _tournaments.Remove(tournament);
            await Task.CompletedTask;
        }



    }
}