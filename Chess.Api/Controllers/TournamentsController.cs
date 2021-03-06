using System;
using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Commands.Tournament;
using Chess.Infrastructure.EF;
using Chess.Infrastructure.Services;
using Chess.Infrastructure.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Chess.Api.Controllers
{
    public class TournamentsController : ApiControllerBase
    {
        private readonly ITournamentService _tournamentService;
        public TournamentsController(ITournamentService tournamentService,
            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _tournamentService = tournamentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tournament = await _tournamentService.BrowseAsync();

            return Ok(tournament);
        }

        // [HttpGet("{name}")]
        // public async Task<IActionResult> Get(string name)
        // {
        //     var tournament = await _tournamentService.GetAsync(name);
        //     if(tournament == null)
        //     {
        //             return NotFound();
        //     }
            
        //     return Ok(tournament);
        // }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var tournament = await _tournamentService.GetAsync(id);
            if(tournament == null)
            {
                    return NotFound();
            }
            
            return Ok(tournament);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTournament command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return Created($"tournaments/{command.Name}",new object());
        }
    }
}