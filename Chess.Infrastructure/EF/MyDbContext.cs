using System.Text.RegularExpressions;
using Chess.Core.Domain;
using Chess.Infrastructure.Extensions;
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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            var tournamentBuilder = builder.Entity<Tournament>();
            tournamentBuilder.HasKey(x => x.Id);
            
            
            base.OnModelCreating(builder);
            foreach(var entity in builder.Model.GetEntityTypes())
            {
                // Replace table names
                entity.Relational().TableName = entity.Relational().TableName.ToSnakeCase();

                // Replace column names            
                foreach(var property in entity.GetProperties())
                {
                    property.Relational().ColumnName = property.Relational().ColumnName.ToSnakeCase();
                }

                foreach(var key in entity.GetKeys())
                {
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();
                }

                foreach(var key in entity.GetForeignKeys())
                {
                    key.Relational().Name = key.Relational().Name.ToSnakeCase();
                }

                foreach(var index in entity.GetIndexes())
                {
                    index.Relational().Name = index.Relational().Name.ToSnakeCase();
                }
            }
        }
        
    }
}