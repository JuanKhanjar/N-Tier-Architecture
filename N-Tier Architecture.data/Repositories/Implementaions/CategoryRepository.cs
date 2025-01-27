using Microsoft.EntityFrameworkCore;
using N_Tier_Architecture.core.Entities;
using N_Tier_Architecture.data.Data;
using N_Tier_Architecture.data.QueryObjects;
using N_Tier_Architecture.data.Repositories.Contracts;

namespace N_Tier_Architecture.data.Repositories.Implementaions
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> FindAsync(CategoryQueryParameters parameters)
        {
            IQueryable<Category> query = _context.Categories;

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
