using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace N_Tier_Architecture.core.Entities
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; } = Guid.NewGuid();

        [Required]
        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; }

        [Required]
        [Column(TypeName = "datetime2")] // Ensures compatibility with SQL Server's datetime2 type
        public DateTime OrderDate { get; set; }

        // Navigation properties
        public Customer? Customer { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; } = [];
    }
}
