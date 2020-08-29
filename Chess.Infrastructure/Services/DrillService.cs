using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Chess.Core.Domain;
using Chess.Core.Repositories;
using Chess.Infrastructure.DTO;

namespace Chess.Infrastructure.Services
{
    public class DrillService : IDrillService
    {
        private readonly IDrillRepository _drillRepository;
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

        public async Task<DrillDto> GetAsync(int id)
        {
            var drill = await _drillRepository.GetAsync(id);
            
            return _mapper.Map<Drill, DrillDto>(drill);
        }
    }
}