using Microsoft.EntityFrameworkCore;
using N_Tier_Architecture.core.Entities;
using N_Tier_Architecture.data.Data;
using N_Tier_Architecture.data.QueryObjects;
using N_Tier_Architecture.data.Repositories.Contracts;

namespace N_Tier_Architecture.data.Repositories.Implementaions
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public ProductRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<Product>> FindAsync(ProductQueryParameters parameters)
        {
            using var context = _contextFactory.CreateDbContext(); // ✅ Correct way to get DbContext instance

            IQueryable<Product> query = context.Products;

            if (parameters.ProductId.HasValue)
                query = query.Where(p => p.ProductId == parameters.ProductId);

            if (!string.IsNullOrEmpty(parameters.ProductName))
                query = query.Where(p => p.ProductName.Contains(parameters.ProductName));

            if (parameters.CategoryId.HasValue)
                query = query.Where(p => p.CategoryId == parameters.CategoryId);

            if (parameters.MinPrice.HasValue)
                query = query.Where(p => p.Price >= parameters.MinPrice);

            if (parameters.MaxPrice.HasValue)
                query = query.Where(p => p.Price <= parameters.MaxPrice);

            if (parameters.IncludeCategory)
                query = query.Include(p => p.Category);

            return await query.ToListAsync();
        }
    }
}
