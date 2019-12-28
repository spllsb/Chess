using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Chess.Infrastructure.DTO;

namespace Chess.Infrastructure.Services
{
    public interface IMatchService : IService
    {

        // TODO 
        //wyswietlam liste meczy dla danego turnieju
        //wyswietlamy liste mezy dla uzytkownika
        //wyswietlamy liste meczy dla klubu
        Task<MatchDto> GetAsync(Guid matchId);
        Task<IEnumerable<MatchDto>> BrowseAsync();
        Task CreateAsync(string title);
    }
}