using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chess.WebSite.Controllers
{
    public class DrillsController : WebControllerBase
    {
        private readonly IDrillService _drillService;
        public DrillsController(IDrillService drillService,
                                ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _drillService = drillService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAllCategory()
        {

            return View(CategoryDrillsEnum.GetValues(typeof(CategoryDrillsEnum)));
        }

        public async Task<IActionResult> GetCategoryListElement(string category)
        {
            var drills = await _drillService.GetAllCategoryElementAsync(category);
            return View(drills);
        }

        public async Task<IActionResult> GetChessDrill()
        {
            var drill = await _drillService.GetAsync();
            return View(drill);
        }
    }

    public enum CategoryDrillsEnum {Gra1, Gra2, Gra3}
    

}