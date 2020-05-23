using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Core.Domain;

namespace Chess.Core.Repositories
{
    public interface IPlayerRepository : IRepository
    {
        Task<Player> GetAsync(Guid id); 
        Task<Player> GetAsync(string username);
        Task<IEnumerable<Player>> GetAllAsync();
        Task AddAsync(Player user);
        Task UpdateAsync(Player user);
        Task RemoveAsync(Guid id);
    }
}