using System;
using System.Threading.Tasks;
using Chess.Infrastructure.Commands;
using Chess.Infrastructure.Commands.Match;
using Chess.Infrastructure.Services;

namespace Chess.Infrastructure.Handlers.Match
{
    public class CreateMatchHandler  : ICommandHandler<CreateMatch>
    {
        private readonly IMatchService _matchService;

        public CreateMatchHandler(IMatchService tournamentService
        )
        {
            _matchService = tournamentService;
        }
        public async Task HandleAsync(CreateMatch command)
        {
            var aa = new Guid();
            var aaa = new Guid();
            System.Console.WriteLine(command.FirstPlayerUserName);
            System.Console.WriteLine(command.SecondPlayerUserName);
            System.Console.WriteLine(command.DuringTime);
            System.Console.WriteLine(command.BeginAt);
            // await _matchService.CreateMatch(aa,aaa, command.BeginAt, command.DuringTime);
        }


    }
}