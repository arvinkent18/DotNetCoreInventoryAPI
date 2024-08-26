using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Inventory.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
              .HasOne<User>()
              .WithMany(u => u.Products)
              .HasForeignKey(p => p.UserId)
              .OnDelete(DeleteBehavior.Cascade);
        }
    }
}