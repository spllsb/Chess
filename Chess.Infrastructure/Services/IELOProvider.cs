using System.Threading.Tasks;
using Chess.Core.Domain.Enum;

namespace Chess.Infrastructure.Services
{
    public interface IELOProvider : IService
    {
        // Function to calculate Elo rating
        void CalcELORating(float Ra, float Rb, ChessGameResultEnum resultPlayerA, ref float refRa, ref float refRb);
    }
}