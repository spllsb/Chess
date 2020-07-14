using System;
using System.ComponentModel.DataAnnotations;

namespace Chess.Infrastructure.Commands.Tournament
{
    public class CreateTournament : ICommand
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "ClubName")]
        public string ClubName { get; set; }
        [Required]
        [Display(Name = "GameTyp")]
        public string GameTyp { get; set; }
        [Required]
        [Display(Name = "StartEvent")]
        public DateTime StartEvent { get; set; }
        [Required]
        [Display(Name = "RoundAmount")]
        public int RoundAmount { get; set; }
        [Required]
        [Display(Name = "Minimalny ranking")]
        public int MinRank { get; set; }
        [Required]
        [Display(Name = "MaxRank")]
        public int MaxRank { get; set; }
        [Required]
        [Display(Name = "MinPlayers")]
        public int MinPlayers { get; set; }
        [Required]
        [Display(Name = "MaxPlayers")]
        public int MaxPlayers { get; set; }
    }
}