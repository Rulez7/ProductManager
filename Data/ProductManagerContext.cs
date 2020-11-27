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
    }
}
