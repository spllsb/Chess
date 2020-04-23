using System.Collections.Generic;
using System.Linq;
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

 
        public async Task<IEnumerable<Drill>> GetAllByCategoryAsync(string category) 
            => await _context.Drills.ToListAsync();

        public async Task<Drill> GetAsync(int id) 
            => await _context.Drills.Where(x=> x.Id == id).FirstOrDefaultAsync();
    }
}