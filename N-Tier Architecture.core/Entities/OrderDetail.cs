using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N_Tier_Architecture.core.Entities
{
    public class OrderDetail
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Order")]
        public Guid OrderId { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total must be greater than 0.")]
        [Column(TypeName = "decimal(18, 2)")] // Explicit precision and scale
        public decimal Total { get; set; }

        // Navigation properties
        public Order? Order { get; set; }=new Order();
        public Product? Product { get; set; }
        
    }
}


/*
 modelBuilder.Entity<OrderDetail>()
    .HasIndex(od => od.ProductId); // Speeds up filtering by product

modelBuilder.Entity<OrderDetail>()
    .HasIndex(od => od.OrderId); // Speeds up joins and filtering by order

modelBuilder.Entity<OrderDetail>()
    .HasIndex(od => new { od.OrderId, od.ProductId }) // Composite index for filtering/order-detail joins
    .IsUnique(); // Ensure uniqueness of order-product combinations

 */
