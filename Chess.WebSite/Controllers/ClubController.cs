using System;
using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chess.WebSite.Controllers
{
    public class ClubController : WebControllerBase
    {

        
        private readonly IClubService _clubService;
        public ClubController(IClubService clubService,
                                    ICommandDispatcher commandDispatcher
                                    ) : base(commandDispatcher)
        {
            _clubService = clubService;
        }

        public async Task<IActionResult> Index(string message, string status, string searchString)
        {
            ViewBag.StatusCode = status;
            ViewBag.Title = searchString;
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
    }
}