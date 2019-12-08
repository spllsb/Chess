using System;
using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Commands.Article;
using Chess.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chess.Api.Controllers
{
    public class ArticlesController : ApiControllerBase
    {
        private readonly IArticleService _articleService;
        public ArticlesController(IArticleService articleService,
            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var articles = await _articleService.BrowseAsync();

            return Json(articles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var article = await _articleService.GetAsync(id);
            if(article == null)
            {
                    return NotFound();
            }
            
            return Json(article);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateArticle command)
        {
            await CommandDispatcher.DispatchAsync(command);
            return Created($"articles/11111",new object());
        }

    }
}