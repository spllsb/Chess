
using System.Threading.Tasks;
using Chess.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chess.WebSite.Views.Shared.Components.PlayerList
{
    public class PlayerListViewComponent : ViewComponent
    {
        private readonly IPlayerService _playerService;

        public PlayerListViewComponent(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string template)
        {
            var a = new PlayerParameters();
            var players = await _playerService.PagedList(a); 
            return View("Default", players);
        }
    }
} 



