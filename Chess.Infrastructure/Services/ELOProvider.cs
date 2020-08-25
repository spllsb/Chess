using System;
using System.Threading.Tasks;
using Chess.Core.Domain.Enum;

namespace Chess.Infrastructure.Services
{
    public class ELOProvider : IELOProvider
    {
        // K is a constant. 
        // d determines which player won. 
        public void CalcELORating(float Ra, float Rb, ChessGameResultEnum resultPlayerA, ref float refRa, ref float refRb)
        {
            float Eb = CalcProbability(Ra, Rb); 
            float Ea = CalcProbability(Rb, Ra);
            int Ka = GetFactorK(Ra); 
            int Kb = GetFactorK(Ra); 
            switch (resultPlayerA){
                //When Player A wins 
                case ChessGameResultEnum.WIN:
                    Ra = Ra + Ka * (1 - Ea); 
                    Rb = Rb + Kb * (0 - Eb); 
                    break;
                //When Player A lose 
                case ChessGameResultEnum.LOSE:
                    Ra = Ra + Ka * (0 - Ea); 
                    Rb = Rb + Kb * (1 - Eb); 
                    break;
                //When Player A Draw 
                case ChessGameResultEnum.DRAW:
                    Ra = Ra + Ka * (0.5f - Ea); 
                    Rb = Rb + Kb * (0.5f - Eb);                     
                    break;
                default:
                    throw new System.ComponentModel.InvalidEnumArgumentException(nameof(resultPlayerA), (int)resultPlayerA, resultPlayerA.GetType());
            }
            refRa = Ra;
            refRb = Rb;
            Console.WriteLine("Ra " + Ra);
            Console.WriteLine("Rb " + Rb);
        }
        private float CalcProbability(float rating1, float rating2)
        => 1.0f * 1.0f / (1 + 1.0f *  (float)(Math.Pow(10, 1.0f * (rating1 - rating2) / 400)));

        private int GetFactorK(float rating)
        => rating>=2400? 10:20;

    }






// 2400 and above: Senior Master
// 2200–2399: National Master
// 2200–2399 plus 300 games above 2200: Original Life Master[10]
// 2000–2199: Expert or Candidate Master
// 1800–1999: Class A
// 1600–1799: Class B
// 1400–1599: Class C
// 1200–1399: Class D
// 1000–1199: Class E
// 800–999: Class F
// 600–799: Class G
// 400–599: Class H
// 200–399: Class I
// 100–199: Class J
}