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
    public class ClubRepository : IClubRepository
    {
        private readonly MyDbContext _context;
        public ClubRepository(MyDbContext context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<Club>> GetAllAsync() 
        => await _context.Clubs.ToListAsync();

        public async Task<Club> GetAsync(string name)
        => await _context.Clubs.Where(x=> x.Name == name).Include(x => x.Players).FirstOrDefaultAsync();

        public IQueryable<Club> FindByCondition(Expression<Func<Club,bool>> expression)
        => _context.Clubs.Where(expression).Include(x => x.Players).AsNoTracking();

        public async Task<Club> GetAsync(Guid id)
        => await _context.Clubs.Where(x=> x.Id == id).Include(x => x.Players).FirstOrDefaultAsync();

        public async Task UpdateAsync(Club club)
        {
            _context.Clubs.Update(club);
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(Club club)
        {
            await _context.Clubs.AddAsync(club);
            await _context.SaveChangesAsync();
        }
    }
}