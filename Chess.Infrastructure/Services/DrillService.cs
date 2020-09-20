using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chess.Core.Domain;
using Chess.Core.Domain.Enum;
using Chess.Core.Repositories;
using Chess.Infrastructure.DTO;
using Microsoft.EntityFrameworkCore;

namespace Chess.Infrastructure.Services
{
    public class DrillService : IDrillService
    {
        private readonly IDrillRepository _drillRepository;
        static Random random = new Random();
        private readonly IMapper _mapper;

        public DrillService(IDrillRepository drillRepository, IMapper mapper)
        {
            _drillRepository = drillRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DrillDto>> GetAllCategoryElementAsync(string category)
        {
            var drills = await _drillRepository.GetAllByCategoryAsync(category);
            return _mapper.Map<IEnumerable<Drill>,IEnumerable<DrillDto>>(drills);
        }

        public async Task<DrillDto> GetAsync(Guid id)
        {
            var drill = await _drillRepository.GetAsync(id);
            
            return _mapper.Map<Drill, DrillDto>(drill);
        }

        public async Task <IEnumerable<DrillDto>> GetDrillAsync()
        {
            var drills = _drillRepository.FindByCondition(x => true);
            return _mapper.Map<IEnumerable<Drill>,IEnumerable<DrillDto>>(drills.ToList());
        }

        public async Task <DrillDto> GetRandomDrillAsync()
        {
            var drills = _drillRepository.FindByCondition(x => true);
            var drillsList = await drills.ToListAsync();
            int r = random.Next(drillsList.Count);
            return _mapper.Map<Drill, DrillDto>(drillsList[r]);
        }

        public async Task AddAsync(Guid drillId, Guid playerId){

        }

        public async Task AddPlayed(Guid drillId, Guid playerId, DrillResultTypeEnum result)
        {
            var playerDrill = PlayerDrillParticipation.Create(playerId,drillId,result);
            var drill = await _drillRepository.GetAsync(drillId);
            drill.AddPlayerAttempt(playerDrill);
            await _drillRepository.Update(drill);
            
        }
    }
}