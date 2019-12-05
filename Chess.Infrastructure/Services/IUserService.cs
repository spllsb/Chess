using System.Threading.Tasks;
using Chess.Infrastructure.DTO;
using Chess.Infrastructure.Services;

namespace Chess.Infrastructure.Service
{
    public interface IUserService : IService
    {
        Task<UserDto> GetAsync(string email);
        Task RegisterAsync(string email, string username, string password);
    }
}