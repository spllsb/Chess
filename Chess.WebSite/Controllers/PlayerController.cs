using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chess.WebSite.Controllers
{
    public class PlayerController : WebControllerBase
    {
        private readonly IPlayerService _playerService;
        public PlayerController(IPlayerService playerService,
                                ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _playerService = playerService;
        }

        public async Task<IActionResult> Index()
        {
            var players = await _playerService.GetAllAsync(); 
            return View(players);
        }
    }
}