using Microsoft.EntityFrameworkCore;
using N_Tier_Architecture.core.Entities;
using N_Tier_Architecture.data.Data;
using N_Tier_Architecture.data.QueryObjects;
using N_Tier_Architecture.data.Repositories.Contracts;

namespace N_Tier_Architecture.data.Repositories.Implementaions
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        //private readonly ApplicationDbContext _context;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        public CategoryRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Category>> FindAsync(CategoryQueryParameters parameters)
        {
            using var context = _contextFactory.CreateDbContext();
            IQueryable<Category> query = context.Categories;

            if (parameters.CategoryId.HasValue)
                query = query.Where(c => c.CategoryId == parameters.CategoryId);

            if (!string.IsNullOrEmpty(parameters.CategoryName))
                query = query.Where(c => c.CategoryName.Contains(parameters.CategoryName));

            if (parameters.IncludeProducts)
                query = query.Include(c => c.Products);
            return await query.ToListAsync();
        }
        
    }
}
