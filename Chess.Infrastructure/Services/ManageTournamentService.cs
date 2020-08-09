using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Chess.Core.Domain;
using Chess.Infrastructure.DTO;
using Chess.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Chess.Infrastructure.Services
{
    public class ManageTournamentService : IManageTournamentService
    {
        private readonly IPlayerService _playerService;
        private readonly IMapper _mapper;
        private readonly MyDbContext _context;

        public ManageTournamentService(IPlayerService playerService,
                                IMapper mapper,
                                MyDbContext context)
        {
            _playerService = playerService;
            _mapper = mapper;
            _context = context;
        }
        public async Task AddPlayerToTournament(Guid tournamentId, string playerName)
        {
            var player = await _playerService.GetAsync(playerName);
            await _context.PlayerTournamentParticipation.AddAsync(PlayerTournamentParticipation.Create(player.PlayerId, tournamentId));
            try
            {
                var count = await _context.SaveChangesAsync();
                if(count == 0)
                {
                    Console.WriteLine("Problem: No changes saved!");
                }
                else
                {
                    Console.WriteLine("Added new record to table PlayerTOurnamentParticipation");
                }  
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
            }
        }

        public async Task<IEnumerable<Guid>> GetPlayersFromTournament(Guid tournamentId)
        => await _context.PlayerTournamentParticipation.Where(x => x.TournamentId == tournamentId).Select(x=>x.PlayerId).ToListAsync();
    }
}