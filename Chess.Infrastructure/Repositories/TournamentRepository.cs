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
    public class TournamentRepository : ITournamentRepository, IDatabaseRepository
    {
        private readonly MyDbContext _context;
        public TournamentRepository(MyDbContext context) 
        {
            _context = context;
        }
        public async Task<Tournament> GetAsync(Guid id)
            => await _context.Tournaments.SingleOrDefaultAsync(x => x.Id == id);


        // public async Task<Tournament> GetAsync(string name)
        //     => await _context.Tournaments.SingleOrDefaultAsync(x => x.Name == name);
        //Dodac paginację (np funkcje Take lub Skip)

        public async Task<Tournament> GetAsync(string name)
        {
            var aa = await _context.Tournaments.SingleOrDefaultAsync(x => x.Name == name);
            Guid id = new Guid("111580e5-1c1f-4b9f-b6db-d6e91d669ae4");
            var xx = _context.Players.SingleOrDefault(x => x.UserId == id);
            var zz = _context.PlayerTournamentParticipation.SingleOrDefault(x => x.TournamentId == id);
        
            return aa;
        }

        public async Task<IEnumerable<Tournament>> GetAllAsync()
        
            => await _context.Tournaments.Take(10).ToListAsync();

        public async Task AddAsync(Tournament user)
        {
            await _context.Tournaments.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Tournament user)
        {
            _context.Tournaments.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task RemoveAsync(Guid id)
        {
            var tournament = await GetAsync(id);
            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();
        }

    }
}