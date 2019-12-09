using System;
using System.Threading.Tasks;
using AutoMapper;
using Chess.Core.Repositories;

namespace Chess.Infrastructure.Services
{
    public class ArticleCommentService : IArticleCommentService
    {
        private IArticleRepository _articleRepository;
        private IMapper _mapper;
        public ArticleCommentService(IArticleRepository articleRepository, 
            IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }
        public async Task AddAsync(Guid articleId, string author, string content)
        {
            var article = await _articleRepository.GetAsync(articleId);
            if(article == null)
            {
                throw new Exception($"Article with id: '{articleId}' don't exists");
            }
            article.AddComment(author,content);
            await _articleRepository.UpdateAsync(article);
            
        }
    }
}