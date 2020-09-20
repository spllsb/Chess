using System;
using System.Threading.Tasks;

namespace Chess.Infrastructure.Services
{
    public class TournamentGeneratorService : ITournamentGeneratorService
    {
        static Random random = new Random();
        public TournamentGeneratorService()
        {
        }
        public void GenerateTournament()
        {
        
        }

        public void RandomWithoutRepeated (){
            int n = 4;
            int k = 24;
            int[] numbers = new int[n];
            for (int i = 0; i < n; i++)
            numbers[i] = i + 1;

            for (int i = 0; i < k; i++)
            {
                // tworzenie losowego indeksu pomiędzy 0 i n - 1
                int r = random.Next(n);
                
                // wybieramy element z losowego miejsca
                Console.WriteLine(numbers[r]);
                
                // przeniesienia ostatniego elementu do miejsca z którego wzięliśmy
                numbers[r] = numbers[n - 1];
                n--;
            }
        }

    }
}