using System.Threading.Tasks;
using Chess.Infrastructure.DTO;

namespace Chess.Infrastructure.Services
{
    public interface IAwardImpService : IService
    {
         Task GetAwardContent(string name);

         Task<bool> CheckAwardByUser(string name, string userName);
         Task<AwardDto> GetAwardDto (string username);
    }
}