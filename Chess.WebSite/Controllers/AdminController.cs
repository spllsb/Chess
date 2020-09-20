using Chess.Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Chess.WebSite.Controllers
{
    public class AdminController : WebControllerBase
    {
        public AdminController(ICommandDispatcher commandDispatcher
                                    ) : base(commandDispatcher)
        {
        }


        public IActionResult Index()
        {
            return View();
        }
        
    }
}