using System;
using System.IO;
using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Commands.Club;
using Chess.Infrastructure.DTO;
using Chess.Infrastructure.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Chess.WebSite.Controllers
{
    public class ClubManagementController : WebControllerBase
    {
        private readonly IClubService _clubService;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment webHostEnvironment;  


        public ClubManagementController(IClubService clubService,
                                    ILoggerFactory loggerFactory,
                                    ICommandDispatcher commandDispatcher,
                                    IWebHostEnvironment hostEnvironment
                                    ) : base(commandDispatcher)
        {
            _clubService = clubService;
            _logger = loggerFactory.CreateLogger<ClubManagementController>();
            webHostEnvironment = hostEnvironment;  
        }

        public async Task<IActionResult> Index(string searchString = null, ClubMessageId? message = null)
        {
            ViewData["StatusMessage"] =
                message == ClubMessageId.UpdateClubSuccess ? "Pomyślnie zaktualizowano utworzony pomyślnie."
                : message == ClubMessageId.Error ? "Bład."
                : "";
            ViewBag.Title = "Zarządzaj klubami";
            ViewBag.Message = message;
            
            var a = new ClubParameters();
            a.Name = searchString;


            var clubs = await _clubService.PagedList(a);         
            return View(clubs);
        }


        public async Task<IActionResult> Details(Guid clubId)
        {
            var clubDetails = await _clubService.GetAsync(clubId);
            if(clubDetails == null)
            {
                return NotFound();
            }
            return View(clubDetails);
        }
        public async Task<IActionResult> Edit(string name)
        {
            ViewBag.Title = "Zarządzaj klubami";
            var clubDetails = await _clubService.GetAsync(name);

            if(clubDetails == null)
            {
                return NotFound();
            }
            return View(clubDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ClubDetailsDto clubDetails)
        {
            if(ModelState.IsValid){
                await _clubService.UpdateAsync(clubDetails);
                return RedirectToAction(nameof(Index), new { message = ClubMessageId.UpdateClubSuccess });
            }
            return View(clubDetails);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateClub command)
        {
            if(ModelState.IsValid)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "img", "clubs");  
                command.UploadsFolder = uploadsFolder;
                await CommandDispatcher.DispatchAsync(command);
                this.HttpContext.Response.StatusCode = 201;
                _logger.LogInformation(3,"Create tournament successfully");
                // return RedirectToAction(nameof(Index), new {Message = ClubMessageId.CreateClubSuccess});
            }
            return View();
        }


        public enum ClubMessageId
        {
            CreateClubSuccess,
            UpdateClubSuccess,
            Error
        }
    }
}

