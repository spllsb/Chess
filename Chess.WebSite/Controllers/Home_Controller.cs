using Chess.Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Chess.WebSite.Controllers
{
    public class HomeController : WebControllerBase
    {
        public HomeController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }

        public IActionResult Index(){
            return View();
        }
    }
}


