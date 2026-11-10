using Microsoft.EntityFrameworkCore;

namespace SemanticClassLibrary.Models
{
    public class ApplicationContext : DbContext
    {
        private readonly string? connectionString;

        public DbSet<Documents> Documents { get; set; }
        public DbSet<SequentialWords> SequentialWords { get; set; }
        public DbSet<UniqueWords> UniqueWords { get; set; }
        public DbSet<Roots> Roots { get; set; }
        public DbSet<Suffixes> Suffixes { get; set; }
        public DbSet<Prefixes> Prefixes { get; set; }
        public DbSet<Endings> Endings { get; set; }

        public ApplicationContext(string? connectionString) : base()
        {
            this.connectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
