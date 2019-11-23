using System.Threading.Tasks;
using Chess.Infrastructure.DTO;
using Chess.Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;

namespace Chess.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{email}")]
        public async Task<UserDto> Get(string email)
                => await _userService.GetAsync(email);

    }
}