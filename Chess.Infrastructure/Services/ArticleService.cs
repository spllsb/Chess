using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Chess.Core.Domain;
using Chess.Core.Repositories;
using Chess.Infrastructure.DTO;

namespace Chess.Infrastructure.Services
{
    public class ArticleService : IArticleService
    {
        
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        public ArticleService(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
            
        }
        public async Task<ArticleDetailsDto> GetAsync(Guid articleId)
        {
            var article = await _articleRepository.GetAsync(articleId);
            if(article == null)
            {
                throw new Exception($"Article with id: '{articleId}' don't exists");
            }
            return _mapper.Map<Article,ArticleDetailsDto>(article);
        }

        public async Task<IEnumerable<ArticleDto>> BrowseAsync()
        {
            var articles = await _articleRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<Article>,IEnumerable<ArticleDto>>(articles);
        }
        
        public async Task CreateAsync(string title, string content, string fullNameAuthor)
        {
            await _articleRepository.AddAsync(new Article(title,content,fullNameAuthor));
        }

        public Task AddCommentAsync(string author, string content)
        {
            throw new NotImplementedException();
        }
    }
}