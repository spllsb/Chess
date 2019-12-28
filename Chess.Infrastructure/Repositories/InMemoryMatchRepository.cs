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
                new Player(new User("user1@email.com", "user1", "secert", "salt")),
                new Player(new User("user10@email.com", "user10", "secert10", "salt10"))
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




    }
}