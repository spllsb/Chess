using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Core.Domain;
using Chess.Infrastructure.DTO;

namespace Chess.Infrastructure.Services
{
    public interface IArticleService : IService
    {
        Task<ArticleDetailsDto> GetAsync(Guid tournamentId);
        Task<IEnumerable<ArticleDto>> BrowseAsync();
        Task CreateAsync(string title,string content,string fullNameAuthor);


        Task AddCommentAsync(string author, string content);
    }
}