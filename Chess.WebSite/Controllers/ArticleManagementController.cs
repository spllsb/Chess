using System;
using System.Linq;
using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Commands.Article;
using Chess.Infrastructure.Commands.Message;
using Chess.Infrastructure.Commands.Tournament;
using Chess.Infrastructure.DTO;
using Chess.Infrastructure.EF;
using Chess.Infrastructure.Services;
using Chess.Infrastructure.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace Chess.WebSite.Controllers
{
    // [Authorize(Roles="Admin")]
    public class ArticleManagementController : WebControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly ILogger _logger;

        public ArticleManagementController(IArticleService tournamentService,
                                            ILoggerFactory loggerFactory,
                                            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _logger = loggerFactory.CreateLogger<ArticleController>();
            _articleService = tournamentService;
        }

        public async Task<IActionResult> Index(ArticleMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == ArticleMessageId.CreateArticleSuccess ? "Artykuł utworzony pomyślnie."
                : message == ArticleMessageId.Error ? "Bład."
                : "";
            ViewBag.Title = "Zarządzaj turniejami";
            ViewBag.Message = message;
            var tournament = await _articleService.BrowseAsync();

            return View(tournament);
        }

        public async Task<IActionResult> Details(Guid articleId)
        {
            var articleDetails = await _articleService.GetAsync(articleId);
            if(articleDetails == null)
            {
                return NotFound();
            }
            return View(articleDetails);
        }
        public async Task<IActionResult> Edit(Guid articleId)
        {
            System.Console.WriteLine(articleId);
            var articleDetails = await _articleService.GetAsync(articleId);
            if(articleDetails == null)
            {
                return NotFound();
            }
            return View(articleDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ArticleDto article)
        {
            if(ModelState.IsValid){
                System.Console.WriteLine(article.Title);
                await _articleService.UpdateAsync(article);
                return RedirectToAction(nameof(Index), new { message = ArticleMessageId.UpdateArticleSuccess });
            }
            return View(article);
        }

        public IActionResult Create()
        {
            ViewBag.Information = "Create new tournament";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateArticle command)
        {
            if(ModelState.IsValid)
            {
                await CommandDispatcher.DispatchAsync(command);
                this.HttpContext.Response.StatusCode = 201;
                _logger.LogInformation(3,"Create article successfully");
                return RedirectToAction(nameof(Index), new {Message = ArticleMessageId.CreateArticleSuccess});
            }
            return View();
        }


        public ActionResult GetTournament(){
            return View();
        }


        public enum ArticleMessageId
        {
            CreateArticleSuccess,
            UpdateArticleSuccess,
            Error
        }
    }
}