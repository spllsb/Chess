using System.Text.RegularExpressions;
using Chess.Core.Domain;
using Chess.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Chess.Infrastructure.EF
{
    public class MyDbContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Tournament> Tournaments { get; set; }
        public virtual DbSet<Article> Articles {get;set;}
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Drill> Drills { get; set; }


        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        }

            // modelBuilder.Entity<Comment>(entity =>
            // {
            //     entity.HasOne(d => d.Article)
            //         .WithMany(p => p.Comments)
            //         .HasForeignKey(d => d.ArticleId);
            // });

            // modelBuilder.Entity<PlayerTournamentParticipation>(entity =>
            // {
            //     entity.HasKey(x => new {x.TournamentId,x.PlayerId});

            //     entity.ToTable("player_tournament_participation");

            //     entity.Property(e => e.PlayerId).HasColumnName("player_id");

            //     entity.Property(e => e.Result)
            //         .IsRequired()
            //         .HasColumnName("result")
            //         .HasMaxLength(10);

            //     entity.Property(e => e.TournamentId).HasColumnName("tournament_id");

            //     entity.HasOne(d => d.Player)
            //         .WithMany(p => p.PlayerTournamentParticipation)
            //         .HasForeignKey(d => d.PlayerId)
            //         .OnDelete(DeleteBehavior.ClientSetNull)
            //         .HasConstraintName("fk_player_id");

            //     entity.HasOne(d => d.Tournament)
            //         .WithMany(p => p.PlayerTournamentParticipation)
            //         .HasForeignKey(d => d.TournamentId)
            //         .OnDelete(DeleteBehavior.ClientSetNull)
            //         .HasConstraintName("fk_tournament_id");
            // });

            // modelBuilder.Entity<Player>(entity =>
            // {
            //     entity.HasKey(e => e.UserId);
            //         // .HasName("players_pkey");

            //     entity.ToTable("players");

            //     entity.Property(e => e.UserId)
            //         .HasColumnName("user_id")
            //         .ValueGeneratedNever();

            //     entity.Property(e => e.Username)
            //         .IsRequired()
            //         .HasColumnName("username")
            //         .HasMaxLength(50);
            // });

            // modelBuilder.Entity<Tournament>(entity =>
            // {
            //     entity.ToTable("tournaments");

            //     entity.Property(e => e.Id)
            //         .HasColumnName("id")
            //         .ValueGeneratedNever();

            //     entity.Property(e => e.MaxPlayers)
            //         .HasColumnName("max_players")
            //         .HasColumnType("numeric");

            //     entity.Property(e => e.Name)
            //         .IsRequired()
            //         .HasColumnName("name")
            //         .HasMaxLength(64);

            //     entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            // });

            // modelBuilder.Entity<User>(entity =>
            // {
            //     entity.ToTable("users");

            //     entity.Property(e => e.Id)
            //         .HasColumnName("id")
            //         .ValueGeneratedNever();

            //     entity.Property(e => e.CreatedAt).HasColumnName("created_at");

            //     entity.Property(e => e.Email)
            //         .IsRequired()
            //         .HasColumnName("email")
            //         .HasMaxLength(50);

            //     entity.Property(e => e.Password)
            //         .IsRequired()
            //         .HasColumnName("password")
            //         .HasMaxLength(50);

            //     entity.Property(e => e.Salt)
            //         .IsRequired()
            //         .HasColumnName("salt")
            //         .HasMaxLength(50);

            //     entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            //     entity.Property(e => e.Username)
            //         .IsRequired()
            //         .HasColumnName("username")
            //         .HasMaxLength(50);
            // });

            // OnModelCreating(modelBuilder);
        // }
    }
}