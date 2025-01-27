using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier_Architecture.data.QueryObjects
{
    public class CustomerQueryParameters
    {
        public Guid? CustomerId { get; set; }  
        public string? AuthUserId { get; set; } 
        public string? Email { get; set; }  
        public bool IncludeOrders { get; set; } = false;
    }
}
