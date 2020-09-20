
using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Infrastructure.DTO;
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

        public async Task<IViewComponentResult> InvokeAsync(string template, string filter)
        {
            IEnumerable<PlayerDto> players = new List<PlayerDto>();
            switch (filter){
                case "Tournament":
                    var a = new PlayerParameters();
                    players = await _playerService.PagedList(a); 
                break;

                case "PlayerRankingInTournament":
                    var b = new PlayerParameters();
                    players = await _playerService.PagedList(b); 

                break;
            }
            return View("Default", players);
        }
    }
} 



