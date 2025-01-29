using Microsoft.EntityFrameworkCore;
using N_Tier_Architecture.core.Entities;
using N_Tier_Architecture.data.Data;
using N_Tier_Architecture.data.QueryObjects;
using N_Tier_Architecture.data.Repositories.Contracts;

namespace N_Tier_Architecture.data.Repositories.Implementaions
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public OrderRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Order>> FindAsync(OrderQueryParameters parameters)
        {
            using var context = _contextFactory.CreateDbContext();

            IQueryable<Order> query = context.Orders;

            if (parameters.OrderId.HasValue)
                query = query.Where(o => o.OrderId == parameters.OrderId);

            if (parameters.CustomerId.HasValue)
                query = query.Where(o => o.CustomerId == parameters.CustomerId);

            if (parameters.StartDate.HasValue)
                query = query.Where(o => o.OrderDate >= parameters.StartDate);

            if (parameters.EndDate.HasValue)
                query = query.Where(o => o.OrderDate <= parameters.EndDate);

            if (parameters.IncludeOrderDetails)
                query = query.Include(o => o.OrderDetails);

            if (parameters.IncludeCustomer)
                query = query.Include(o => o.Customer);

            return await query.ToListAsync();
        }
    }
}
