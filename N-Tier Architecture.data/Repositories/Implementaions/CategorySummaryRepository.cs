using Microsoft.EntityFrameworkCore;
using N_Tier_Architecture.core.Entities;
using N_Tier_Architecture.data.Data;
using N_Tier_Architecture.data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier_Architecture.data.Repositories.Implementaions
{
    public class CategorySummaryRepository : ICategorySummaryRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public CategorySummaryRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<CategoryProductsSummary?> GetByCategoryIdAsync(Guid categoryId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.CategoryProductsSummaries.FindAsync(categoryId);
        }
    }

}
