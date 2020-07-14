using System.Threading.Tasks;
using Chess.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chess.WebSite.Views.Shared.Components.ChessgameList
{
    public class ChessgameListViewComponent : ViewComponent
    {       
        private readonly IMatchService _matchService;
        public ChessgameListViewComponent(IMatchService matchService)
        {
            _matchService = matchService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string card_title)
        {
            ViewBag.CardTitle = card_title;

            var matches = await _matchService.BrowseAsync();
            return View("Default", matches);
        }
    }
}