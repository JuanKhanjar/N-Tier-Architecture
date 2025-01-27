using N_Tier_Architecture.core.Entities;
using N_Tier_Architecture.data.QueryObjects;

namespace N_Tier_Architecture.data.Repositories.Contracts
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<Category>> FindAsync(CategoryQueryParameters parameters);
    }
}
