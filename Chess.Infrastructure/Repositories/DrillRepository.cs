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
    public class DrillRepository : IDrillRepository, IDatabaseRepository
    {
        private readonly MyDbContext _context;
        public DrillRepository(MyDbContext context) 
        {
            _context = context;
        }
        public async Task<Drill> GetAsync(Guid id) 
            => await _context.Drills.Where(x=> x.Id == id).Include(x => x.Players).AsNoTracking().FirstOrDefaultAsync();

        public IQueryable<Drill> FindByCondition(Expression<Func<Drill,bool>> expression)
            => _context.Drills.Where(expression).Include(x => x.Players).AsNoTracking();


        public async Task<IEnumerable<Drill>> GetAllByCategoryAsync(string category) 
            => await _context.Drills.ToListAsync();


    }
}