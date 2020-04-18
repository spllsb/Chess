using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Chess.WebSite.Controllers
{
    public class ChessDrillsController : WebControllerBase
    {
        public ChessDrillsController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllCategory()
        {

            return View();
        }

        public IActionResult GetCategoryListElement()
        {
            return View();
        }

        public IActionResult GetChessDrill()
        {
            return View();
        }
    }

    public enum CategoryDrillsEnum {Gra1, Gra2, Gra3}
    

}