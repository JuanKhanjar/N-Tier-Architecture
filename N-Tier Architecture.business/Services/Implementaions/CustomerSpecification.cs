using N_Tier_Architecture.business.Services.Contracts;
using N_Tier_Architecture.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier_Architecture.business.Services.Implementaions
{
    public class CustomerSpecification : ISpecification<Customer>
    {
        public Expression<Func<Customer, bool>> Criteria { get; }

        public CustomerSpecification(Guid? customerId = null, string? authUserId = null, string? email = null)
        {
            if (customerId != null)
            {
                Criteria = c => c.CustomerId == customerId;
            }
            else if (!string.IsNullOrEmpty(authUserId) && !string.IsNullOrEmpty(email))
            {
                Criteria = c => c.AuthUserId == authUserId && c.CustomerEmail == email;
            }
            else if (!string.IsNullOrEmpty(authUserId))
            {
                Criteria = c => c.AuthUserId == authUserId;
            }
            else
            {
                Criteria = c => true; // Or throw an exception
            }
        }
    }

}
