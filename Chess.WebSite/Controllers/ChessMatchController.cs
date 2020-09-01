using System;
using System.IO;
using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Services;
using Chess.Infrastructure.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Chess.WebSite.Controllers
{
    [Authorize]
    public class ChessMatchController : WebControllerBase
    {
        private readonly ChessGameSettings _chessGameSettings;
        private readonly IFileProvider _fileProvider;

        public ChessMatchController(ICommandDispatcher commandDispatcher,
                                    IOptions<ChessGameSettings> chessGameSettings,
                                    IFileProvider fileProvider) : base(commandDispatcher)
        {
            _fileProvider = fileProvider;
            _chessGameSettings = chessGameSettings.Value;
        }

        public IActionResult Index()
        {

            return View();
        }
        public IActionResult ChessGame(Guid id, string duration)
        {
            ViewBag.ChessGameDuration = duration;
            ViewBag.ChessGameId = id;
            return View();
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
            ViewBag.pgn = pgn.Replace(System.Environment.NewLine, "");
            // ViewBag.pgn = pgn.Replace(System.Environment.NewLine, " ");
            return View();
        }
    }
}