using System;
using System.ComponentModel.DataAnnotations;

namespace Chess.Infrastructure.DTO
{
    public class TournamentDto
    {
        public Guid Id { get; set;}

        [Display(Name = "Nazwa turnieju")]
        public string Name { get; set; }
        [Display(Name = "Rozpoczęcie turnieju")]
        public DateTime StartEvent { get; set; }
        [Display(Name = "Minimalna liczba graczy")]
        public int MinPlayers { get; set; }
        [Display(Name = "Maksymalna liczba graczy")]
        public int MaxPlayers { get; set; }
        [Display(Name = "Minimalna ranking graczy")]
        public int MinRank { get; set; }
        [Display(Name = "Maksymalny ranking graczy")]
        public int MaxRank { get; set; }
        [Display(Name = "Liczba uczestników")]
        public int RegisteredPlayersCount {get; set;}

    }
}