using System;
using System.ComponentModel.DataAnnotations;

namespace Chess.Infrastructure.DTO
{
    public class ClubDto
    {
        public Guid Id { get; set; }
        [Display(Name = "Nazwa klubu")]
        public string Name { get; set; }
        [Display(Name = "Email kontaktowy")]
        [EmailAddress]  
        public string ContactEmail { get; set; }
        [Display(Name = "Liczba uczestników")] 
        public int PlayersCount { get; set; }
        [Display(Name = "Utworzony")]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Zdjęcie")]
        public string PictureName { get; set; }

    }
}