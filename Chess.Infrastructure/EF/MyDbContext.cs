using System;
using Chess.Core.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chess.Infrastructure.EF
{
    public class MyDbContext : IdentityDbContext<ApplicationUser>
    {
        // public virtual DbSet<User> Users { get; set; }
        
        public virtual DbSet<Tournament> Tournaments { get; set; }
        public virtual DbSet<Article> Articles {get;set;}
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Drill> Drills { get; set; }
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers {get; set; }
        public virtual DbSet<PlayerTournamentParticipation> PlayerTournamentParticipation {get; set;}
        public virtual DbSet<Club> Clubs { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(x => x.UserId);
            });

            modelBuilder.Entity<PlayerTournamentParticipation>(entity =>
            {
                entity.HasKey(x => new {x.TournamentId,x.PlayerId});
                entity.HasOne(d => d.Player)
                    .WithMany(p => p.Tournaments)
                    .HasForeignKey(d => d.PlayerId);

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.Players)
                    .HasForeignKey(d => d.TournamentId);
            });


            modelBuilder.Entity<ApplicationUser>(b =>
            {
                b.ToTable("asp_net_users");
            });
        }

    }

}