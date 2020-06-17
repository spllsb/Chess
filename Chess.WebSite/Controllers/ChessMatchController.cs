using System;
using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chess.WebSite.Controllers
{
    [Authorize]
    public class ChessMatchController : WebControllerBase
    {
        public ChessMatchController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
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
    
        public IActionResult SearchGame(int durationTime){
            //available player list 
            
            return View();
        }
    }
}