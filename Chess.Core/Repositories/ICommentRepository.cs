using System;
using System.Threading.Tasks;
using Chess.Core.Domain;

namespace Chess.Core.Repositories
{
    public interface ICommentRepository : IRepository
    
    {
        Task<Comment> GetAsync(Guid id); 
        Task AddAsync(Comment comment);
        Task UpdateAsync(Comment comment);
        Task RemoveAsync(Guid id);
    }
}