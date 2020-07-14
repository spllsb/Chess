using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chess.Core.Domain;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.DTO;
using Chess.Infrastructure.EF;
using Chess.WebSite.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chess.Infrastructure.Services
{
    public class TournamentController : WebControllerBase
    {
        // private readonly MyDbContext _context; //test

        private readonly ITournamentService _tournamentService;

        public TournamentController(ITournamentService tournamentService,
                                    // MyDbContext context, //test
                                    ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _tournamentService = tournamentService;
            // _context = context; //test
        }

        public async Task<IActionResult> Index(string message, string status, string searchString)
        {
            ViewBag.StatusCode = status;
            ViewBag.Title = searchString;
            ViewBag.Message = message;
            // !string.IsNullOrEmpty(searchString) ? View(await _tournamentService.GetAsync(searchString)) : View (await _tournamentService.BrowseAsync());

            var a = new TournamentParameters(searchString);


            var tournaments = await _tournamentService.PagedList(a);         
            // //test
            // var aa = _context.Tournaments
            //     .Skip(1 * 1-0)
            //     .Take(10);
            // return Ok(await aa.ToArrayAsync());
            return View(tournaments);
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







        // private void SearchByName(ref IEnumerable<TournamentDto> tournaments, string ownerName)
        // {
        //     if (!tournaments.Any() || string.IsNullOrWhiteSpace(ownerName))
        //         return;

        //     tournaments = tournaments.Where(o => o.Name.ToLower().Contains(ownerName.Trim().ToLower()));
        //     }
        }
}