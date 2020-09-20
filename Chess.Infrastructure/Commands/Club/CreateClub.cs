using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Chess.Infrastructure.Commands.Club
{
    public class CreateClub : ICommand
    {
        [Required]
        [Display(Name = "Nazwa klubu")]
        public string Name { get; set; }
        [Display(Name = "Email kontaktowy")]
        [EmailAddress]  
        [Required]
        public string ContactEmail { get; set; }

        [Display(Name = "Informacje")]
        [Required]
        public string Informaction { get; set; }

        [Display(Name = "Zdjącie")]  
        public IFormFile ProfileImage { get; set; }  
        
        public string UploadsFolder { get; set; }        
    }
}