using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Chess.Core.Domain;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Services;
using Chess.Infrastructure.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Chess.WebSite.Controllers
{
    [Authorize]
    public class ChessMatchController : WebControllerBase
    {
        private readonly ChessGameSettings _chessGameSettings;
        private readonly IFileProvider _fileProvider;
        private readonly IPlayerService _playerService;
        private readonly UserManager<ApplicationUser> _userManager;
        public ChessMatchController(ICommandDispatcher commandDispatcher,
                                    IOptions<ChessGameSettings> chessGameSettings,
                                    IPlayerService playerService,
                                    UserManager<ApplicationUser> userManager,
                                    IFileProvider fileProvider) : base(commandDispatcher)
        {
            _userManager = userManager;
            _fileProvider = fileProvider;
            _playerService = playerService;
            _chessGameSettings = chessGameSettings.Value;
        }

        public async Task<IActionResult> Index()
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            var player = await _playerService.GetAsync(username);
            ViewBag.PlayerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(player);
        }


        public IActionResult ChessGame_copy(Guid id, string duration)
        {
            ViewBag.ChessGameDuration = duration;
            ViewBag.ChessGameId = id;
            return View();
        }
    
        public IActionResult SearchGame(int durationTime){
            //available player list 
            return View();
        }


        public async Task<IActionResult> ChessGameRepeat(string path)
        {
            Console.WriteLine(path);
            var pgn = await _fileProvider.GetFileContent(_chessGameSettings.PGNFilePath,path);
            // ViewBag.pgn = pgn.Replace(System.Environment.NewLine, "");
            ViewBag.pgn = pgn.Replace(System.Environment.NewLine, "\\n");
            // ViewBag.pgn = pgn.Replace(System.Environment.NewLine, " ");
            return View();
        }
    }
}