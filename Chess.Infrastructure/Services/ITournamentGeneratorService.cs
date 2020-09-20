using System.Threading.Tasks;

namespace Chess.Infrastructure.Services
{
    public interface ITournamentGeneratorService : IService
    {
        void GenerateTournament();
        void RandomWithoutRepeated();
    }
}