using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Chess.Core.Domain.Enum;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Services;
using Chess.Infrastructure.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Chess.WebSite.Controllers
{
    public class DrillsController : WebControllerBase
    {
        private readonly IDrillService _drillService;
        private readonly IFileProvider _fileProvider;
        private readonly DrillSettings _drillSettings;
        private readonly DrillAwardService _chessGameAwardService;

        public DrillsController(IDrillService drillService,
                                IFileProvider fileProvider,
                                IOptions<DrillSettings> drillSettings,
                                IAwardImpService chessGameAwardService,
                                ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _drillSettings = drillSettings.Value;
            _fileProvider = fileProvider;
            _drillService = drillService;
            _chessGameAwardService = (DrillAwardService)chessGameAwardService;
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

        public async Task<IActionResult> GetChessDrill(Guid? id)
        {
            var drillAward = await _chessGameAwardService.GetAwardDto("konto@gmail.com");
            if(drillAward != null){
                ViewData["awardImgFileName"] = drillAward.FileName;
                ViewData["awardContent"] = drillAward.Comment;
            }

            var drill = id != null ?  await _drillService.GetAsync(id.Value) : await _drillService.GetRandomDrillAsync();
            System.Console.WriteLine(drill.Id);
            var pgn = await _fileProvider.GetFileContent(_drillSettings.FilePath, drill.FileName);
            ViewBag.pgn = pgn.Replace(System.Environment.NewLine, "\\n");

            ViewBag.CorrectlyAttempts = drill.Attempts > 0 ? (100*drill.CorrectlyAttempts)/drill.Attempts : 0;
            return View(drill);
        }

        public async Task<IActionResult> Test()
        {
            var drill = await _drillService.GetDrillAsync();
            return View(drill);
        }



        [HttpPost]
        public async Task SaveDrill(string Result, string DrillId){
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userIdGuid = new Guid(userId);
            var drillIdGuid = new Guid(DrillId);
            DrillResultTypeEnum result = Result == "1" ? DrillResultTypeEnum.CORRECT : DrillResultTypeEnum.INCORRECT;
            await _drillService.AddPlayed(drillIdGuid, userIdGuid, result);
        }
    }

    public enum CategoryDrillsEnum {Gra1, Gra2, Gra3}
    

}