using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Commands.Tournament;
using Chess.Infrastructure.Services;

namespace Chess.Infrastructure.Handlers.Tournaments
{
    public class CreateTournamentHandler : ICommandHandler<CreateTournament>
    {
        private readonly ITournamentService _tournamentService;

        public CreateTournamentHandler(ITournamentService tournamentService
        )
        {
            _tournamentService = tournamentService;
        }
        public async Task HandleAsync(CreateTournament command)
        {
            await _tournamentService.CreateAsync(command.Name,command.MaxPlayers);
        }
    }
}