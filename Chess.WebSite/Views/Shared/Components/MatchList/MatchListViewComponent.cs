
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Infrastructure.DTO;
using Chess.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chess.WebSite.Views.Shared.Components.MatchList
{
    public class MatchListViewComponent : ViewComponent
    {
        private readonly IMatchService _matchService;
        private IEnumerable<MatchDto> matches;

        public MatchListViewComponent(IMatchService matchService)
        {
            _matchService = matchService;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid id, string fromTable)
        {
            switch(fromTable)
            {
                case "player":
                    matches = await _matchService.GetByPlayerAsync(id);
                    return View("MatchesByPlayer",matches);
                case "tournament":
                    matches = await _matchService.GetByTournamentAsync(id);
                    return View("MatchesByTournament",matches);
                default:
                    return Content("Not found case from matchlist component");
            }
        }
    }
} 



