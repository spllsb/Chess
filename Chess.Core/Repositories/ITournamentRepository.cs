using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Core.Domain;

namespace Chess.Core.Repositories
{
    public interface ITournamentRepository : IRepository
    {
        Task<Tournament> GetAsync(Guid id); 
        Task<IEnumerable<Tournament>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task RemoveAsync(Guid id);
    }
}