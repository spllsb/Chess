namespace Chess.Infrastructure.Commands.Tournament
{
    public class CreateTournament : ICommand
    {
        public string Name { get; set; }
        public int MaxPlayers { get; set; }
    }
}