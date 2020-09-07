using System;
using System.Linq;
using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Commands.Message;
using Chess.Infrastructure.Commands.Tournament;
using Chess.Infrastructure.EF;
using Chess.Infrastructure.Services;
using Chess.Infrastructure.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace Chess.WebSite.Controllers
{
    public class TournamentManagementController : WebControllerBase
    {
        private readonly ITournamentService _tournamentService;
        private readonly ILogger _logger;

        public TournamentManagementController(ITournamentService tournamentService,
                                            ILoggerFactory loggerFactory,
                                            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _logger = loggerFactory.CreateLogger<TournamentManagementController>();
            _tournamentService = tournamentService;
        }
    // ITournamentService tournamentService,
    // public TournamentController(
    //     ICommandDispatcher commandDispatcher) : base(commandDispatcher)
    // {
    //     // _tournamentService = tournamentService;
    // }

    // [HttpGet]
        public async Task<IActionResult> Index(TournamentMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == TournamentMessageId.CreateTournamentSuccess ? "Turniej utworzony pomyślnie."
                : message == TournamentMessageId.Error ? "Bład."
                : "";
            ViewBag.Title = "Zarządzaj turniejami";
            ViewBag.Message = message;
            var tournament = await _tournamentService.BrowseAsync();

            return View(tournament);
        }

        public async Task<IActionResult> Details(string name)
        {
            var tournamentDetails = await _tournamentService.GetAsync(name);
            if(tournamentDetails == null)
            {
                return NotFound();
            }
            return View(tournamentDetails);
        }

        public IActionResult Create()
        {
            ViewBag.Information = "Create new tournament";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateTournament command)
        {
            if(ModelState.IsValid)
            {
                await CommandDispatcher.DispatchAsync(command);
                this.HttpContext.Response.StatusCode = 201;
                _logger.LogInformation(3,"Create tournament successfully");
                return RedirectToAction(nameof(Index), new {Message = TournamentMessageId.CreateTournamentSuccess});
            }
            return View();
        }


        public ActionResult GetTournament(){
            return View();
        }



        public async Task<IActionResult> SendMessage(string name)
        {
            var tournament = await _tournamentService.GetAsync(name);
            if(tournament == null)
            {
                return NotFound();
            }
            ViewBag.PlayersDropDown = new MultiSelectList(tournament.Players,"Email","Username",tournament.Players.Select(x => x.Email));
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(SendMessage command)
        {
            if(ModelState.IsValid)
            {
                await CommandDispatcher.DispatchAsync(command);
            }
            else
            {
                throw new Exception("No input parameter");
            }
            return RedirectToAction("Index");
        }
        public enum TournamentMessageId
        {
            CreateTournamentSuccess,
            Error
        }
    }
}