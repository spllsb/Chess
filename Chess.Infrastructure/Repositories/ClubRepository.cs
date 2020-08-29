using System.Collections.Generic;
using System.Linq;
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
        => await _context.Clubs.Where(x=> x.Name == name).FirstOrDefaultAsync();

           

           

    }
}