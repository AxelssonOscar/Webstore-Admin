using Microsoft.EntityFrameworkCore;
using Webstore_Admin.Data.Configurations;
using Webstore_Admin.Models;

namespace Webstore_Admin.Data.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<DiscountProduct> DiscountProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            modelBuilder.Entity<DiscountProduct>()
            .HasKey(dp => new { dp.DiscountId, dp.ProductId });
                modelBuilder.Entity<DiscountProduct>()
                    .HasOne(dp => dp.Discount)
                    .WithMany(d => d.DiscountProducts)
                    .HasForeignKey(bc => bc.DiscountId);
                modelBuilder.Entity<DiscountProduct>()
                .HasOne(dp => dp.Product)
                .WithMany(p => p.DiscountProducts)
                .HasForeignKey(dp => dp.ProductId);

        }
    }
}
