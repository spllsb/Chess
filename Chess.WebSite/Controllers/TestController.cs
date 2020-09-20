using System;
using System.Threading.Tasks;
using Chess.Core.Domain;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Chess.WebSite.Controllers
{
    public class TestController : WebControllerBase
    {
        private RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITournamentGeneratorService _testService;
        private readonly IMatchService _matchService;
        private readonly ILogger _logger;

        public TestController(ITournamentGeneratorService testService,
                            ICommandDispatcher commandDispatcher,
                            IMatchService matchService,
                            RoleManager<IdentityRole> roleMgr,
                            UserManager<ApplicationUser> userManager) : base(commandDispatcher)
        {
            _testService = testService;
            _matchService = matchService;
            roleManager = roleMgr;
            _userManager = userManager;
        }


        public async Task <IActionResult> Index()
        {
            //Create new role
            // IdentityResult result = await roleManager.CreateAsync(new IdentityRole("Admin"));
            // if (result.Succeeded)
            //     return Content("ok");
            // else
            //     return Content(result.ToString());



            //Create new user to group
            // var user = new ApplicationUser();
            // user.UserName = "admin";
            // user.Email = "admin@admin.com";

            // string userPWD = "1qaz@WSX";
            // IdentityResult chkUser = await _userManager.CreateAsync(user, userPWD);
            // if (chkUser.Succeeded){
            //     var result1 = await _userManager.AddToRoleAsync(user, "Admin");
            //     return Content("ok");
            // }
            // else
            //     return Content(chkUser.ToString());

           return Content("ok");
        }

    }
}