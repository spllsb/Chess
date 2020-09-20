using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Chess.Core.Domain;
using Chess.Core.Repositories;
using Chess.Infrastructure.DTO;
using Microsoft.AspNetCore.Identity;

namespace Chess.Infrastructure.Services
{
    public class ChessGameAwardService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPlayerService _playerService;
        public ChessGameAwardService(IPlayerService playerService,
                                    UserManager<ApplicationUser> userManager)
        {
            _playerService = playerService;
            _userManager = userManager;
        }
        public async Task GetAwardContent(string name)
        {
            // var username = _userManager.GetUserNameAsync();
            // var player = await _playerService.GetAsync(username);
            // switch(name){
            //     case "Żółtodziób w puzzle":

            //         // _playerService.GetAsync(Conte);

            //     break;
            // }
        }
        public async Task CheckAwardByUser(string name, string userName)
        {
            System.Console.WriteLine("i am in");
            // var player = await _playerService.GetAsync(username);
            // switch(name){
            //     case "Żółtodziób w puzzle":

            //         // _playerService.GetAsync(Conte);

            //     break;
            // }
        }


    }
}
