using System;

namespace Chess.Infrastructure.DTO
{
    public class PlayerDto
    {
        public Guid PlayerId { get; set; }
        public string UserName { get; set; }
        public Guid ClubId {get; set;}
        public string Email { get; set; }
        public int RatingElo { get; set; }
        public int Result { get; set; }
        public string AvatarImageName { get; set; }
    }
}