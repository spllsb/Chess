using System;
using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Commands.Article;
using Chess.Infrastructure.DTO;
using Chess.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpGet]
        [Route("{articleId}")]
        public async Task<IActionResult> Get(Guid articleId)
        {
            var article = await _articleService.GetAsync(articleId);
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