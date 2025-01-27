using N_Tier_Architecture.core.Entities;
using N_Tier_Architecture.data.QueryObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier_Architecture.data.Repositories.Contracts
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IEnumerable<Customer>> FindAsync(CustomerQueryParameters parameters);
    }
}
