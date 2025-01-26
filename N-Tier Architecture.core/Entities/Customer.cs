using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier_Architecture.core.Entities
{
    public class Customer
    {
        [Key]
        public Guid CustomerId { get; set; } = Guid.NewGuid();

        //[Required]
        //public string AuthUserId { get; set; } // Unique user identifier from the Authentication API


        [Required]
        [StringLength(100, ErrorMessage = "Customer name cannot exceed 100 characters.")]
        public required string CustomerName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public required string CustomerEmail { get; set; }

        // Navigation property
        public ICollection<Order>? Orders { get; set; }=new List<Order>();
    }
}
