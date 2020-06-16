using Chess.Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Chess.WebSite.Controllers
{
    public class Home_Controller : WebControllerBase
    {
        public Home_Controller(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }

        public IActionResult Index(){
            
            return View();
        }
    }
}


