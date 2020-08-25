using System.Threading.Tasks;

namespace Chess.Infrastructure.Services
{
    public interface IPGNProvider : IService
    {
        Task<string> GetPGNContent();
    }
}