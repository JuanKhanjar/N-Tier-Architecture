using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N_Tier_Architecture.core.Entities
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }= Guid.NewGuid();

        [Required]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public required string ProductName { get; set; }

        public string? ProductDescription { get; set; }

        public string? ProductImageUrl { get; set; } //if it is null then provide a default image


        [Required]
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        [Column(TypeName = "decimal(18, 2)")] // Explicit precision and scale
        public decimal Price { get; set; }

        // Navigation properties
        public Category? Category { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; } = [];
    }
}
/*
 modelBuilder.Entity<Product>()
   .HasIndex(p => p.CategoryId); // Speeds up queries filtering by category

modelBuilder.Entity<Product>()
   .HasIndex(p => new { p.ProductName, p.Price }); // Composite index for filtering and sorting by name and price

 */