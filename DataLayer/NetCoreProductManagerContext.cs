using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ModelLayer;

namespace DataLayer
{
    public class NetCoreProductManagerContext : IdentityDbContext<ApplicationUser>
    {
        public NetCoreProductManagerContext(DbContextOptions<NetCoreProductManagerContext> options) : base(options)
        {

        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Category>().ToTable("Category");
        }
    }
}
