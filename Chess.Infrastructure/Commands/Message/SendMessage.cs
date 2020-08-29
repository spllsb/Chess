using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chess.Infrastructure.Commands.Message
{
    public class SendMessage  : ICommand
    {
        [Required]
        [Display(Name = "Lista odbiorców")]
        public IEnumerable<string> SendToList { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Treść")]
        public string Content { get; set; }
    }
}