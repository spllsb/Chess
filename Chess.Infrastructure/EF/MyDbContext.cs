using Chess.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Chess.Infrastructure.EF
{
    public class MyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }

        // private readonly DatabaseSettings _databaseSettings;

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var tournamentBuilder = modelBuilder.Entity<Tournament>();
            tournamentBuilder.HasKey(x => x.Id);
        }
    }
}