using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Core.Domain;

namespace Chess.Core.Repositories
{
    public interface IArticleRepository : IRepository 
    {
        Task<Article> GetAsync(Guid id);
        Task<IEnumerable<Article>> GetAllAsync();
        Task AddAsync(Article article);
        Task UpdateAsync(Article article);
        Task RemoveAsync(Guid id);


        //wyswietl komentarze
    }
}