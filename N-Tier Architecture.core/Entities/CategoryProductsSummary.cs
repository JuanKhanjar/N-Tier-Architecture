using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier_Architecture.core.Entities
{
    public class CategoryProductsSummary
    {
        [Key]
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }

        public int ProductCount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalProductValue { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal AverageProductPrice { get; set; }

        public DateTime UpdatedAt { get; set; }
        public Category? Category { get; set; }
    }

}
