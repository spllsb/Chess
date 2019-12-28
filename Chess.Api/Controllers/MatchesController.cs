using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chess.Api.Controllers
{
    public class MatchesController : ApiControllerBase
    {
        private readonly IMatchService _matchService;
        public MatchesController(ICommandDispatcher commandDispatcher,
            IMatchService matchService) : base(commandDispatcher)
        {
            _matchService = matchService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var articles = await _matchService.BrowseAsync();

            return Ok(articles);
        }
    
    }
}