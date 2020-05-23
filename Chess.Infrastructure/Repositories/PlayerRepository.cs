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
    public class PlayerRepository : IPlayerRepository
    {
        private readonly MyDbContext _context;
        public PlayerRepository(MyDbContext context) 
        {
            _context = context;
        }
        public async Task<Player> GetAsync(Guid id)
            => await _context.Players.SingleOrDefaultAsync(x => x.UserId == id);
        public async Task<Player> GetAsync(string username)
            => await _context.Players.SingleOrDefaultAsync(x => x.Username == username);

        public async Task<IEnumerable<Player>> GetAllAsync()
            => await _context.Players.Take(10).ToListAsync();

        public async Task AddAsync(Player Player)
        {
            await _context.Players.AddAsync(Player);
            await _context.SaveChangesAsync();
        }
        public Task UpdateAsync(Player Player)
        {
            throw new NotImplementedException();
        }
        public async Task RemoveAsync(Guid id)
        {
            var Player = await GetAsync(id);
            _context.Remove(Player);
            await _context.SaveChangesAsync();
        }
    }
}