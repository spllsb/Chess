using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Chess.WebSite.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Chess.Infrastructure.Services
{
    public class TournamentController : WebControllerBase
    {
        private readonly ITournamentService _tournamentService;

        public TournamentController(ITournamentService tournamentService,
                                    ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _tournamentService = tournamentService;
        }

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
    }
}