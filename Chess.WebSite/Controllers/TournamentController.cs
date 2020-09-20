using System;
using System.Threading.Tasks;
using Chess.Core.Domain;
using Chess.Core.Domain.Enum;
using Chess.Infrastructure.Commands;
using Chess.WebSite.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Chess.Infrastructure.Services
{
    public class TournamentController : WebControllerBase
    {

        private readonly ITournamentService _tournamentService;
        private readonly IManageTournamentService _manageTournamentService;

        public TournamentController(ITournamentService tournamentService,
                                    IManageTournamentService manageTournamentService,
                                    ICommandDispatcher commandDispatcher
                                    ) : base(commandDispatcher)
        {
            _tournamentService = tournamentService;
            _manageTournamentService = manageTournamentService;
        }

        public async Task<IActionResult> Index(string message, string status, string searchString)
        {
            ViewBag.Title = "Turnieje";

            ViewBag.StatusCode = status;
            ViewBag.Message = message;
            var parameters = new TournamentParameters();
            parameters.Name = searchString;
            parameters.Status  = TournamentStatusEnum.created; 
            var tournaments = await _tournamentService.PagedList(parameters);         
            return View(tournaments);
        }




        public async Task<IActionResult> CompletedTournament()
        {
            ViewBag.Title = "Zakończone turnieje";
            
            var parameters = new TournamentParameters();
            parameters.Status  = TournamentStatusEnum.complete; 
            var tournaments = await _tournamentService.PagedList(parameters);   

            return View(tournaments);
        }





   
        public async Task<IActionResult> Details(Guid tournamentId)
        {
            var tournamentDetails = await _tournamentService.GetAsync(tournamentId);
            if(tournamentDetails == null)
            {
                return NotFound();
            }
            return View(tournamentDetails);
        }


        public async Task<IActionResult> CompletedTournamentDetails(Guid tournamentId)
        {
            var tournamentDetails = await _tournamentService.GetAsync(tournamentId);
            if(tournamentDetails == null)
            {
                return NotFound();
            }
            return View(tournamentDetails);
        }

        public async Task<IActionResult> Join(Guid tournamentId)
        {
            await _manageTournamentService.AddPlayerToTournament(tournamentId, this.User.Identity.Name);
            return NoContent();
            // return RedirectToAction("Details");P
        }

        // private void SearchByName(ref IEnumerable<TournamentDto> tournaments, string ownerName)
        // {
        //     if (!tournaments.Any() || string.IsNullOrWhiteSpace(ownerName))
        //         return;

        //     tournaments = tournaments.Where(o => o.Name.ToLower().Contains(ownerName.Trim().ToLower()));
        //     }
        }
}