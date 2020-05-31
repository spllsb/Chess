using System;
using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chess.WebSite.Controllers
{
    public class MatchController : WebControllerBase
    {
        private readonly IMatchService _matchService;
        public MatchController( IMatchService matchService,
                                ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _matchService = matchService;
        }

        public async Task<ActionResult> Index()
        {
            var matches = await _matchService.BrowseAsync();
            return View(matches);
        }


        public async Task<ActionResult> GetPlayerMatch(Guid id)
        {
            var matches = await _matchService.GetByPlayerAsync(id);
            return View(matches);
        }

        public async Task<ActionResult> GetMatchOfTournament()
        {
            var matches = await _matchService.BrowseAsync();
            return View(matches);
        }
        
    }
}
