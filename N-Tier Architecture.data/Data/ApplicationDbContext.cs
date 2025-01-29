using Microsoft.EntityFrameworkCore;
using N_Tier_Architecture.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier_Architecture.data.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<CategoryProductsSummary> CategoryProductsSummaries { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite Key for OrderDetail
            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => new { od.OrderId, od.ProductId });

            // Product-Category Relationship
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Order-Customer Relationship
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // OrderDetail-Order Relationship
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId);

            // OrderDetail-Product Relationship
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId);

            // تكوين العلاقة بين `Category` و `Products`
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // تكوين العلاقة بين `Category` و `CategoryProductsSummary`
            modelBuilder.Entity<CategoryProductsSummary>()
                .HasOne(s => s.Category)
                .WithOne(c => c.Summary)
                .HasForeignKey<CategoryProductsSummary>(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);  // حذف الملخص عند حذف الفئة



            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(o => o.OrderDate)
                      .HasColumnType("datetime2")
                      .IsRequired()
                      .HasDefaultValueSql("GETUTCDATE()"); // Sets the default value to the current UTC time
            });

            // Seed data (if needed)
        }

    }
}
