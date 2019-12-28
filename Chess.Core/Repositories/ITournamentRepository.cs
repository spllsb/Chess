using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Core.Domain;

namespace Chess.Core.Repositories
{
    public interface ITournamentRepository : IRepository
    {
        Task<Tournament> GetAsync(Guid id); 
        Task<Tournament> GetAsync(string name); 
        Task<IEnumerable<Tournament>> GetAllAsync();
        Task AddAsync(Tournament tournament);
        Task UpdateAsync(Tournament tournament);
        Task RemoveAsync(Guid id);
    }
}