using System.Linq;
using AutoMapper;
using Chess.Core.Domain;
using Chess.Core.Domain.Enum;
using Chess.Infrastructure.DTO;

namespace Chess.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
                => new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Tournament,TournamentDto>()
                        .ForMember(x => x.RegisteredPlayersCount, o => o.MapFrom(x => x.Players.Count()));
                    cfg.CreateMap<Tournament,TournamentDetailsDto>()
                        .ForMember(x => x.RegisteredPlayersCount, o => o.MapFrom(x => x.Players.Count()));
                        
                    cfg.CreateMap<PlayerTournamentParticipation,PlayerDto>()
                        .ForMember(x => x.PlayerId, opt => opt.MapFrom(y => y.Player.UserId))
                        .ForMember(x => x.UserName, opt => opt.MapFrom(y => y.Player.UserName))
                        .ForMember(x => x.Email, opt => opt.MapFrom(y => y.Player.Email))
                        .ForMember(x => x.ClubId, opt => opt.MapFrom(y => y.Player.ClubId))
                        .ForMember(x => x.RatingElo, opt => opt.MapFrom(y => y.Player.RatingElo))
                        .ForMember(x => x.AvatarImageName, opt => opt.MapFrom(y => y.Player.AvatarImageName))
                        .ForMember(x => x.Result, opt => opt.MapFrom(y => y.Result));
                    // cfg.CreateMap<PlayerDto,PlayerInCompleteTournamentDto>();
                    // cfg.CreateMap<PlayerTournamentParticipation,PlayerInCompleteTournamentDto>()


                    // cfg.CreateMap<User,UserDto>();
                    cfg.CreateMap<Article,ArticleDto>();
                    cfg.CreateMap<Article,ArticleDetailsDto>();
                    cfg.CreateMap<Comment,CommentDto>();

                    cfg.CreateMap<Match,MatchDto>();
                    cfg.CreateMap<Award,AwardDto>()
                        .ForMember(x => x.IsComplite, opt => opt.MapFrom(y => y.Players.Count() > 0));
                    cfg.CreateMap<Player,PlayerDto>()
                        .ForMember(x => x.PlayerId, opt => opt.MapFrom(y => y.UserId));
                    cfg.CreateMap<Drill,DrillDto>()
                        .ForMember(x => x.Attempts, opt => opt.MapFrom(y => y.Players.Count()))
                        .ForMember(x => x.CorrectlyAttempts, opt => opt.MapFrom( y => y.Players.Where( z => z.Result == DrillResultTypeEnum.CORRECT.ToString()).Count()));
                    cfg.CreateMap<Club,ClubDto>()
                        .ForMember(x => x.PlayersCount, opt => opt.MapFrom(y => y.Players.Count()));
                    cfg.CreateMap<Club,ClubDetailsDto>();

                    cfg.CreateMap<Player,PlayerDetailsDto>()
                        .ForMember(x => x.ResolveDrillsCount, opt => opt.MapFrom(y => y.Drills.Count()))
                        .ForMember(x => x.CorrectResolveDrillsCount, opt => opt.MapFrom(y => y.Drills.Where(z => z.Result == DrillResultTypeEnum.CORRECT.ToString()).Count()));
                })
                .CreateMapper();
    }
}