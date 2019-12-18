
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

    public class UserRepository : IUserRepository, IDatabaseRepository
    {
        private readonly MyDbContext _context;
        public UserRepository(MyDbContext context) 
        {
            _context = context;
        }
        public async Task<User> GetAsync(Guid id)
            => await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
        public async Task<User> GetAsync(string email)
            => await _context.Users.SingleOrDefaultAsync(x => x.Email == email);

        public async Task<IEnumerable<User>> GetAllAsync()
            => await _context.Users.Take(10).ToListAsync();

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
        public async Task RemoveAsync(Guid id)
        {
            var user = await GetAsync(id);
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}