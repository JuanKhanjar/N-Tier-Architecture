using N_Tier_Architecture.core.Entities;

namespace N_Tier_Architecture.data.Repositories.Contracts
{
    public interface ICategorySummaryRepository
    {
        Task<CategoryProductsSummary?> GetByCategoryIdAsync(Guid categoryId);
    }

}
