using Chess.Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Chess.WebSite.Controllers
{
    public class Account_Controller : WebControllerBase
    {
        public Account_Controller(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }
    
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LogIn()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

    }
}