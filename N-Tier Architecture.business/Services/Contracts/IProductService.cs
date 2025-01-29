using N_Tier_Architecture.core.Entities;
using N_Tier_Architecture.data.QueryObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier_Architecture.business.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(Guid productId);
        Task<IEnumerable<Product>> FindProductsAsync(ProductQueryParameters parameters);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Guid productId);
    }


}
