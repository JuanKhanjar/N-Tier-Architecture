using N_Tier_Architecture.core.Entities;
using N_Tier_Architecture.data.QueryObjects;

namespace N_Tier_Architecture.data.Repositories.Contracts
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> FindAsync(ProductQueryParameters parameters);
    }
}
