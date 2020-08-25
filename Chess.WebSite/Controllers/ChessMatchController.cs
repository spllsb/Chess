using System;
using System.IO;
using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Chess.WebSite.Controllers
{
    [Authorize]
    public class ChessMatchController : WebControllerBase
    {
        private readonly IPGNProvider _pgnProvider;

        public ChessMatchController(ICommandDispatcher commandDispatcher,
                                    IPGNProvider pgnProvider) : base(commandDispatcher)
        {
            _pgnProvider = pgnProvider;
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
        public IActionResult ChessGame_v2(Guid id, string duration)
        {
            ViewBag.ChessGameDuration = duration;
            ViewBag.ChessGameId = id;
            return View();
        }
    
        public IActionResult SearchGame(int durationTime){
            //available player list 
            return View();
        }


        public async Task<IActionResult> Test()
        {
            var pgn = await _pgnProvider.GetPGNContent();
            ViewBag.pgn = pgn.Replace(System.Environment.NewLine, "");
            // ViewBag.pgn = pgn.Replace(System.Environment.NewLine, " ");
            return View();
        }
    }
}