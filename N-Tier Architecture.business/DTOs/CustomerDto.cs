using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier_Architecture.business.DTOs
{
    public class CustomerDto
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public List<OrderDto>? Orders { get; set; }
    }

}
/*
 * Ideal Layer for DTOs
DTOs should be placed in a separate folder under the Business Layer, as they represent data contracts between the Service Layer and the API Controllers.
 Why Put DTOs in the Business Layer?
✅ Encapsulation – Prevents exposing the internal domain models directly.
✅ Separation of Concerns – Keeps DTOs separate from Database Entities.
✅ Better API Structure – Allows easy modifications to API responses without affecting the database
 */