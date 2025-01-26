using N_Tier_Architecture.business.Services.Contracts;
using N_Tier_Architecture.core.Entities;
using N_Tier_Architecture.data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier_Architecture.business.Services.Implementaions
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _unitOfWork.Products.GetAllAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductWithIncludesAsync()
        {
            var products = await _unitOfWork.Products.GetAllWithIncludesAsync(p => p.Category);
            return products;
        }

        public async Task<Product?> GetProductByIdAsync(Guid productId)
        {
            return await _unitOfWork.Products.GetByIdAsync(productId);
        }

        public async Task AddProductAsync(Product product)
        {
            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            var existingProduct = await _unitOfWork.Products.GetByIdAsync(product.ProductId);
            if (existingProduct == null) throw new KeyNotFoundException("Product not found");

            existingProduct.ProductName = product.ProductName;
            existingProduct.ProductDescription = product.ProductDescription;
            existingProduct.Price = product.Price;
            existingProduct.ProductImageUrl = product.ProductImageUrl;
            existingProduct.CategoryId = product.CategoryId;

            _unitOfWork.Products.Update(existingProduct);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteProductAsync(Guid productId)
        {
            var product = await _unitOfWork.Products.GetByIdAsync(productId);
            if (product == null) throw new KeyNotFoundException("Product not found");

            _unitOfWork.Products.Delete(product);
            await _unitOfWork.SaveAsync();
        }

    }
}
