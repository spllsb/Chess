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
        public async Task<ActionResult> Create(CreateTournament command)
        {
            if(ModelState.IsValid)
            {
                await CommandDispatcher.DispatchAsync(command);
                this.HttpContext.Response.StatusCode = 201;
                return RedirectToAction("Index", new {message ="JUPI udalo sie utworzyc nowy turniej", status = "success"});
            }
            ViewBag.Message = "NIe udalo sie stworzyc turnieju";
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
    }
}