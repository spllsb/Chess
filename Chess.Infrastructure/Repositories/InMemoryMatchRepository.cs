using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chess.Core.Domain;
using Chess.Core.Repositories;

namespace Chess.Infrastructure.Repositories
{
    public class InMemoryMatchRepository : IMatchRepository
    {
        private static ISet<Match> _matches = new HashSet<Match>
        {
            Match.Create(
                new Player(new User("user1@email.com", "user3", "secert", "salt"))
                ),
            Match.Create(
                new Player(new User("user2@email.com", "user2", "secert2", "salt2"))
                )
        };
        public async Task<IEnumerable<Match>> GetAllAsync()
            => await Task.FromResult(_matches);
        public async Task<Match> GetAsync(Guid id)
            => await Task.FromResult(_matches.SingleOrDefault(x => x.Id == id));
        public async Task AddAsync(Match match)
        {
            _matches.Add(match);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Match>> GetMatchByPlayerAsync(Guid playerId)
            => await Task.FromResult(_matches.Where(x => x.PlayerId == playerId));
    }
}