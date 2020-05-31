using System;
using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Commands.Tournament;
using Chess.Infrastructure.EF;
using Chess.Infrastructure.Services;
using Chess.Infrastructure.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Chess.WebSite.Controllers
{
    public class TournamentManagementController : WebControllerBase
    {
        private readonly ITournamentService _tournamentService;

        public TournamentManagementController(ITournamentService tournamentService,
            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _tournamentService = tournamentService;
        }
    // ITournamentService tournamentService,
    // public TournamentController(
    //     ICommandDispatcher commandDispatcher) : base(commandDispatcher)
    // {
    //     // _tournamentService = tournamentService;
    // }

    // [HttpGet]
        public async Task<IActionResult> Index(string message, string status)
        {
            ViewBag.StatusCode = status;
            ViewBag.Title = "Tournaments";
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
        public ActionResult Create(CreateTournament command)
        {
            if(ModelState.IsValid)
            {
                // await CommandDispatcher.DispatchAsync(command);
                this.HttpContext.Response.StatusCode = 201;
                return RedirectToAction("Index", new {message ="JUPI udalo sie utworzyc nowy turniej", status = "success"});
            }
            ViewBag.Message = "NIe udalo sie stworzyc turnieju";
            return View();
        }


        public ActionResult GetTournament(){
            return View();
        }
    }
}