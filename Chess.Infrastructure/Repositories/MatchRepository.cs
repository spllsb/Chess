using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chess.Core.Domain;
using Chess.Core.Repositories;
using Chess.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Chess.Infrastructure.Repositories
{
    public class MatchRepository : IMatchRepository
    {        
        private readonly MyDbContext _context;
        public MatchRepository(MyDbContext context) 
        {
            _context = context;
        }
        //  public async Task<IEnumerable<Match>> GetAllAsync()
        //     => await _context.Matches.Include(x => x.Player).ToListAsync();
        public async Task<Match> GetAsync(Guid id)
            => await _context.Matches.SingleOrDefaultAsync(x => x.Id == id);
        public async Task AddAsync(Match match)
        {
            _context.Matches.Add(match);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Match>> GetMatchByPlayerAsync(Guid playerId)
            => await _context.Matches.Where(x => x.FirstPlayerId == playerId || x.SecondPlayerId == playerId).Include(x => x.FirstPlayer).Include(x => x.SecondPlayer).ToListAsync();

        public Task<IEnumerable<Match>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}