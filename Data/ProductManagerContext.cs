using ProductManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ProductManager.Data
{
    class ProductManagerContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "Server=.;Database=ProductManager;Trusted_Connection=True";

            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Article>().HasIndex(s => s.Name).IsUnique();

            modelBuilder.Entity<Category>().HasIndex(s => s.Name).IsUnique();
        }
    }
}
