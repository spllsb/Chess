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
    public class AwardRepository : IAwardRepository, IDatabaseRepository
    {
        private readonly MyDbContext _context;
        public AwardRepository(MyDbContext context)
        {
            _context = context;
        }

        public IQueryable<Award> FindByCondition(Expression<Func<Award, bool>> expression)
        => _context.Awards.Where(expression).Include(x => x.Players).AsNoTracking();

        public async Task<IEnumerable<Award>> GetAllAsync()
        =>await _context.Awards.Include(x => x.Players).AsNoTracking().ToListAsync();

        public async Task<Award> GetAsync(string name)
        =>await _context.Awards.Where(x => x.Name == name).AsNoTracking().FirstOrDefaultAsync();  
    }
}