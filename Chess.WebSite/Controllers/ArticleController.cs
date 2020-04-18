using System;
using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chess.WebSite.Controllers
{
    public class ArticleController : WebControllerBase
    {
        private readonly IArticleService _articleService;
        public ArticleController(IArticleService articleService,
                                ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _articleService = articleService;
        }
    

        public IActionResult Index()
        {
            return View();
        }
        
    
        public async Task<IActionResult> GetPreviewArticles()
        {
            var articles = await _articleService.BrowseAsync();
            return View(articles);

        }

        public async Task<IActionResult> GetArticle(Guid id)
        {
            var article = await _articleService.GetAsync(id);
            return View(article);
        }
    }
}