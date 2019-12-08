using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Infrastructure.DTO;

namespace Chess.Infrastructure.Services
{
    public interface IArticleService : IService
    {
        Task<ArticleDto> GetAsync(Guid tournamentId);
        Task<IEnumerable<ArticleDto>> BrowseAsync();
        Task CreateAsync(string title,string content,string fullNameAuthor);
    }
}