using AutoMapper;
using Chess.Core.Domain;
using Chess.Infrastructure.DTO;

namespace Chess.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
                => new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Tournament,TournamentDto>();
                    cfg.CreateMap<Tournament,TournamentDetailsDto>();
                    cfg.CreateMap<User,UserDto>();
                })
                .CreateMapper();
    }
}