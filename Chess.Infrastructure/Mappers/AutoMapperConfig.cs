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
                    cfg.CreateMap<PlayerTournamentParticipation,PlayerDto>()
                        .ForMember(x => x.PlayerId, opt => opt.MapFrom(y => y.Player.UserId))
                        .ForMember(x => x.Username, opt => opt.MapFrom(y => y.Player.Username))
                        .ForMember(x => x.Email, opt => opt.MapFrom(y => y.Player.Email))
                        .ForMember(x => x.ClubId, opt => opt.MapFrom(y => y.Player.ClubId));
        
                    cfg.CreateMap<User,UserDto>();
                    cfg.CreateMap<Article,ArticleDto>();
                    cfg.CreateMap<Article,ArticleDetailsDto>();
                    cfg.CreateMap<Comment,CommentDto>();

                    cfg.CreateMap<Match,MatchDto>();
                    cfg.CreateMap<Player,PlayerDto>()
                        .ForMember(x => x.PlayerId, opt => opt.MapFrom(y => y.UserId));
                    cfg.CreateMap<Drill,DrillDto>();
                    cfg.CreateMap<Club,ClubDto>();
                })
                .CreateMapper();
    }
}