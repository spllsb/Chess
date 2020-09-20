using System.Threading.Tasks;
using AutoMapper;
using Chess.Core.Repositories;
using Chess.Infrastructure.DTO;

namespace Chess.Infrastructure.Services
{
    public class DrillAwardService : IAwardImpService
    {
        private readonly IAwardService _awardService;
        private readonly IMapper _mapper;
        private readonly IPlayerService _playerService;
        public DrillAwardService(IAwardService awardService, IMapper mapper, IPlayerService playerService) 
        {
            _awardService = awardService;
            _mapper = mapper;
            _playerService = playerService;
        }

        public async Task<bool> CheckAwardByUser(string name, string userName)
        {
            var player = await _playerService.GetDetailsAsync(userName);
            if(player.CorrectResolveDrillsCount == 0)
            {
                return true;
            }
            return false;
        }

        public Task GetAwardContent(string name)
        {
            throw new System.NotImplementedException();
        }

        public async Task<AwardDto> GetAwardDto(string username)
        {
            var player = await _playerService.GetDetailsAsync(username);
            if(player.CorrectResolveDrillsCount == 0){
                return await _awardService.GetAsync("Żółtodziób w puzzle");
            }
            return null;
        }

        private enum DrillAwardName{
            PuzzleNull, Puzzle1
        }   
    }
}