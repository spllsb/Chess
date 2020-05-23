using Chess.Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Chess.WebSite.Controllers
{
    public class StatisticController : WebControllerBase
    {
        public StatisticController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }

        public IActionResult Index(){
            return View();
        } 
    }
}