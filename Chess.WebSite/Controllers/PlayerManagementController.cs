using System;
using System.Linq;
using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Commands.Message;
using Chess.Infrastructure.Commands.Tournament;
using Chess.Infrastructure.DTO;
using Chess.Infrastructure.EF;
using Chess.Infrastructure.Services;
using Chess.Infrastructure.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace Chess.WebSite.Controllers
{
    // [Authorize(Roles="Admin")]
    public class PlayerManagementController : WebControllerBase
    {
        private readonly IPlayerService _playerService;
        private readonly ILogger _logger;

        public PlayerManagementController(IPlayerService playerService,
                                            ILoggerFactory loggerFactory,
                                            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _logger = loggerFactory.CreateLogger<PlayerManagementController>();
            _playerService = playerService;
        }
    // IPlayerService playerService,
    // public TournamentController(
    //     ICommandDispatcher commandDispatcher) : base(commandDispatcher)
    // {
    //     // _playerService = playerService;
    // }

    // [HttpGet]
        public async Task<IActionResult> Index(string searchString = null, PlayerMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == PlayerMessageId.CreatePlayerSuccess ? "Turniej utworzony pomyślnie."
                : message == PlayerMessageId.Error ? "Bład."
                : "";
            ViewBag.Title = "Zarządzaj użytkownikami";
            ViewBag.Message = message;
            
            var param = new PlayerParameters();
            param.Name = searchString;
            var players = await _playerService.PagedList(param); 
            return View(players);
        }

        public async Task<IActionResult> Details(string name)
        {
            var playerDto = await _playerService.GetAsync(name);
            if(playerDto == null)
            {
                return NotFound();
            }
            return View(playerDto);
        }
        public async Task<IActionResult> Edit(string userName)
        {
            var player = await _playerService.GetAsync(userName);

            System.Console.WriteLine(player.RatingElo);

            return View(player);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PlayerDto playerDto)
        {
            if(ModelState.IsValid){
                await _playerService.UpdateAsync(playerDto);
                return RedirectToAction(nameof(Index), new { message = PlayerMessageId.UpdatePlayerSuccess });
            }
            return View(playerDto);
        }

        public enum PlayerMessageId
        {
            CreatePlayerSuccess,
            UpdatePlayerSuccess,
            Error
        }
    }
}