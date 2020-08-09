
using System;
using System.Threading.Tasks;
using Chess.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chess.WebSite.Views.Shared.Components.MatchList
{
    public class MatchListViewComponent : ViewComponent
    {
        private readonly IMatchService _matchService;

        public MatchListViewComponent(IMatchService matchService)
        {
            _matchService = matchService;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid id, string fromTable)
        {
            var maches = await _matchService.GetByPlayerAsync(id);
            return View("Default",maches);
        }
    }
} 



