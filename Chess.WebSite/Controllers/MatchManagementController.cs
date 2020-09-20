using System;
using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Commands.Match;
using Chess.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Chess.WebSite.Controllers
{
    public class MatchManagementController : WebControllerBase
    {
  
        private readonly IMatchService _matchService;
        private readonly ILogger _logger;

        public MatchManagementController(IMatchService matchService,
                                            ILoggerFactory loggerFactory,
                                            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _logger = loggerFactory.CreateLogger<MatchManagementController>();
            _matchService = matchService;
        }
        
        
        public async Task<IActionResult> MatchListByTournament(Guid tournamentId, MatchMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == MatchMessageId.CreateMatchSuccess ? "Mecz utworzony pomyślnie."
                : message == MatchMessageId.UpdateMatchSuccess ? "Mecz aktualizowany pomyślnie."
                : message == MatchMessageId.Error ? "Bład."
                : "";
            ViewBag.Title = "Zarządzanie meczami";
            ViewBag.Message = message;
            var matches = await _matchService.GetByTournamentAsync(tournamentId);
            return View(matches);
        }

        public async Task<IActionResult> MatchListByPlayer(Guid playerId, MatchMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == MatchMessageId.CreateMatchSuccess ? "Mecz utworzony pomyślnie."
                : message == MatchMessageId.UpdateMatchSuccess ? "Mecz aktualizowany pomyślnie."
                : message == MatchMessageId.Error ? "Bład."
                : "";
            ViewBag.Title = "Zarządzanie meczami";
            ViewBag.Message = message;
            var matches = await _matchService.GetByPlayerAsync(playerId);
            return View(matches);
        }


        public async Task<IActionResult> MatchListByTournamentRound(Guid tournamentId, int round, MatchMessageId? message = null)
        {

            var parameters = new MatchParameters();
            parameters.TournamentId = tournamentId;  
            parameters.Round = 1;
            var matches = await _matchService.PagedList(parameters);
            return View(matches);
        }




        public async Task<IActionResult> Create(Guid id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody]CreateMatch command)
        {
            if(ModelState.IsValid)
            {
                await CommandDispatcher.DispatchAsync(command);
                this.HttpContext.Response.StatusCode = 201;
                _logger.LogInformation(3,"Create tournament successfully");
                return RedirectToAction(nameof(Index), new {Message = MatchMessageId.CreateMatchSuccess});
            }
            return View();
        }


        public async Task<IActionResult> Edit(Guid id)
        {
            id = new Guid("354d9c91-2c45-4ddf-9988-5dce39b39a8f");
            var matchDetails = await _matchService.GetAsync(id);
            if(matchDetails == null)
            {
                return NotFound();
            }
            return View(matchDetails);
        }

        public enum MatchMessageId
        {
            CreateMatchSuccess,
            UpdateMatchSuccess,
            Error
        }
        
    }
}