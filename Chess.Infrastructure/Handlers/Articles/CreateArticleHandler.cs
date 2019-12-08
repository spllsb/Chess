using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Commands.Article;
using Chess.Infrastructure.Services;

namespace Chess.Infrastructure.Handlers.Articles
{
    public class CreateArticleHandler : ICommandHandler<CreateArticle>
    {
        private readonly IArticleService _articleService;

        public CreateArticleHandler(IArticleService ArticleService
        )
        {
            _articleService = ArticleService;
        }
        public async Task HandleAsync(CreateArticle command)
        {
            await _articleService.CreateAsync(command.Title, command.Content, command.FullNameAuthor);
        }
    }
}