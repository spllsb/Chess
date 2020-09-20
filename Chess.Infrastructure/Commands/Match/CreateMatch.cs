using System;
using System.ComponentModel.DataAnnotations;

namespace Chess.Infrastructure.Commands.Match
{
    public class CreateMatch : ICommand
    {
        [Required]
        [DataType(DataType.Text)]
        public string FirstPlayerUserName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string SecondPlayerUserName { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime BeginAt { get; set; }
        [Required]
        public int DuringTime { get; set; }
    }
}