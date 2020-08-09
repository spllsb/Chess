using System;
using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chess.WebSite.Controllers
{
    public class MemberController : WebControllerBase
    {
        private readonly IPlayerService _playerService;
        public MemberController(IPlayerService playerService,
                                ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _playerService = playerService;
        }
    
        public async Task<IActionResult> Index()
        {
            var member = await _playerService.GetAsync("bujnolukasz123@gmail.com");
            return View(member);
        }
    }
}