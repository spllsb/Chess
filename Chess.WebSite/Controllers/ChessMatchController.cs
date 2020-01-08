using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Chess.WebSite.Controllers
{
    public class ChessMatchController : WebControllerBase
    {
        public ChessMatchController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}