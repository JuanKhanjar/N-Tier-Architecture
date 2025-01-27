using N_Tier_Architecture.core.Entities;
using N_Tier_Architecture.data.QueryObjects;

namespace N_Tier_Architecture.data.Repositories.Contracts
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> FindAsync(OrderQueryParameters parameters);
    }
}
