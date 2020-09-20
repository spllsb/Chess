using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chess.Core.Domain;
using Chess.Core.Repositories;
using Chess.Infrastructure.DTO;
using Microsoft.EntityFrameworkCore;

namespace Chess.Infrastructure.Services
{
    public class AwardService : IAwardService
    {
        private readonly IAwardRepository _awardRepository;
        private readonly IMapper _mapper;

        public AwardService(IAwardRepository awardRepository, 
                            IMapper mapper)
        {
            _awardRepository = awardRepository;
            _mapper = mapper;
        }



        // Task GetAllAsync();
        // Task GetByPlayerAsync();
        // Task GetNotResolveAsync();



        // public async Task <IEnumerable<AwardDto>> PagedList( parameters)
        // {
        //     var awards = _awardRepository.FindByCondition(x => x.Category == parameters.Category.ToString());

        //     var awardsAfterPagination =  awards
        //         .Skip((parameters.PageNumber - 1) * parameters.PageSize)
        //         .Take(parameters.PageSize)
        //         .ToList();
        //     return _mapper.Map<IEnumerable<Award>,IEnumerable<AwardDto>>(awardsAfterPagination);
        // }

        public async Task<IEnumerable<AwardDto>> GetAllByCategoryAsync(string category) 
        {
            var awards = await _awardRepository.FindByCondition(x=> x.Category == category).ToListAsync();
            return _mapper.Map<IEnumerable<Award>,IEnumerable<AwardDto>>(awards);
        }

        public async Task<IEnumerable<AwardDto>> GetAllAsync()
        {
            var awards = await _awardRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<Award>,IEnumerable<AwardDto>>(awards);
        }

        public async Task<AwardDto> GetAsync(string name)
        {
            var awards = await _awardRepository.GetAsync(name);
            return _mapper.Map<Award,AwardDto>(awards);
        }
    }

}

