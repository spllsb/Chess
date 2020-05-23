using System;
using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Commands.Users;
using Chess.Infrastructure.DTO;
using Chess.Infrastructure.Service;
using Chess.Infrastructure.Settings;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Chess.WebSite.Controllers

{
    public class UsersController : WebControllerBase
    {

        private readonly IUserService _userService;
        public UsersController(IUserService userService,
            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _userService = userService;
        }


        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllAsync(); 
            return View(users);
        }


        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            var user = await _userService.GetAsync(email);
            if(user == null)
            {
                    return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] CreateUser command)
        {
            await CommandDispatcher.DispatchAsync(command);
            return Created($"users/{command.Email}",new object());
        }
    }
}