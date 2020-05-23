using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Core.Domain;

namespace Chess.Core.Repositories
{
    public interface IRoom
    {
        Task<Room> GetAsync(Guid id); 
        Task<IEnumerable<Room>> GetAllAsync();
        Task AddAsync(Room tournament);
        Task UpdateAsync(Room tournament);
        Task RemoveAsync(Guid id);
    }
}