using Microsoft.EntityFrameworkCore;
using PRODUCT_CATALOG_SPA.Models;

namespace PRODUCT_CATALOG_SPA.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.ProductName)
                .IsUnique(); // prevent duplicates
        }
    }
}
