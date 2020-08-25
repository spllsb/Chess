using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Chess.Core.Domain;
using Chess.Core.Repositories;
using Chess.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Chess.Infrastructure.Repositories
{
    public class TournamentRepository : ITournamentRepository, IDatabaseRepository
    {
        private readonly MyDbContext _context;
        public TournamentRepository(MyDbContext context) 
        {
            _context = context;
        }
        // public async Task<Tournament> GetAsync(Guid id)
        //     {    
        //         await _context.Tournaments.Include(x => x.PlayerTournamentParticipation).SingleOrDefaultAsync(x => x.Id == id);
        //     }

        public async Task<Tournament> GetAsync(Guid id)
            => await _context.Tournaments.Include(x => x.Players).ThenInclude(x=>x.Player).SingleOrDefaultAsync(x => x.Id == id);

        public async Task<Tournament> GetAsync(string name)
            => await _context.Tournaments.Where(x => x.Name == name).Include(x => x.Players).ThenInclude(x=>x.Player).SingleOrDefaultAsync();
        //Dodac paginację (np funkcje Take lub Skip)
        public async Task<Tournament> GetTournamentAsync(string name)
            => await _context.Tournaments.Where(x => x.Name == name).FirstOrDefaultAsync();
        public async Task<IEnumerable<Tournament>> GetAllAsync()
        
            => await _context.Tournaments.Take(10).ToListAsync();

        public async Task AddAsync(Tournament tournament)
        {
            await _context.Tournaments.AddAsync(tournament);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Tournament tournament)
        {
            _context.Tournaments.Update(tournament);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveAsync(Guid id)
        {
            var tournament = await GetAsync(id);
            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Tournament> FindByCondition(Expression<Func<Tournament,bool>> expression)
            => _context.Tournaments.Where(expression).AsNoTracking();

        public  Task<Tournament> GetDetailsTournament(Guid id)
        {
            throw new NotImplementedException();
            // var tournament = await _context.Tournaments
            //     .Include(x => x.Matches)
            //     .SingleOrDefaultAsync(x => x.Id == id);
            // return await Task.FromResult(tournament);
        }
    }
}