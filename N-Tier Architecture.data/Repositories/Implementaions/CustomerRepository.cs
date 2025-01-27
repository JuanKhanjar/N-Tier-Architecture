using Microsoft.EntityFrameworkCore;
using N_Tier_Architecture.core.Entities;
using N_Tier_Architecture.data.Data;
using N_Tier_Architecture.data.QueryObjects;
using N_Tier_Architecture.data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier_Architecture.data.Repositories.Implementaions
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> FindAsync(CustomerQueryParameters parameters)
        {
            IQueryable<Customer> query = _context.Customers;

            if (parameters.CustomerId.HasValue)
                query = query.Where(c => c.CustomerId == parameters.CustomerId);

            if (!string.IsNullOrEmpty(parameters.AuthUserId))
                query = query.Where(c => c.AuthUserId == parameters.AuthUserId);

            if (!string.IsNullOrEmpty(parameters.Email))
                query = query.Where(c => c.CustomerEmail == parameters.Email);

            if (parameters.IncludeOrders)
                query = query.Include(c => c.Orders);

            return await query.ToListAsync();
        }
    }
}
