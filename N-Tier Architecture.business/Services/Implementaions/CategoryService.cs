using N_Tier_Architecture.business.DTOs;
using N_Tier_Architecture.business.Services.Contracts;
using N_Tier_Architecture.core.Entities;
using N_Tier_Architecture.data.QueryObjects;
using N_Tier_Architecture.data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier_Architecture.business.Services.Implementaions
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync();

            var categoryDtos = categories.Select(c =>
            {
                var productDtos = c.Products?.Any() == true
                    ? c.Products.Select(p => new ProductDto
                    {
                        ProductId = p.ProductId,
                        ProductName = p.ProductName,
                        ProductDescription = p.ProductDescription,
                        ProductImageUrl = p.ProductImageUrl,
                        Price = p.Price,
                        CategoryId = p.CategoryId,
                        CategoryName = c.CategoryName  
                    }).ToList()
                    : [];

                return new CategoryDto
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    CategoryDescription = c.CategoryDescription,
                    CategoryImageUrl = c.CategoryImageUrl,
                    Products = productDtos
                };
            });

            return categoryDtos;
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(Guid categoryId)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);

            // If category is null, return null
            if (category == null)
                return null;
            var summary = await _unitOfWork.CategorySummaries.GetByCategoryIdAsync(categoryId);


            // Check if Products exist, otherwise return an empty list
            var productDtos = category.Products?.Any() == true
                ? category.Products.Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    ProductImageUrl = p.ProductImageUrl,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    CategoryName = category.CategoryName
                }).ToList()
                : new List<ProductDto>(); // Return an empty list instead of `null`

            // Return the DTO
            return new CategoryDto
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                CategoryDescription = category.CategoryDescription,
                CategoryImageUrl = category.CategoryImageUrl,
                Products = productDtos,
                ProductCount = summary?.ProductCount ?? 0,
                TotalProductValue = summary?.TotalProductValue ?? 0,
                AverageProductPrice = summary?.AverageProductPrice ?? 0
            };
        }

        public async Task<IEnumerable<CategoryDto>> FindCategoriesAsync(CategoryQueryParameters parameters)
        {
            var categories = await _unitOfWork.Categories.FindAsync(parameters);

            return categories.Select(c => new CategoryDto
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
                CategoryDescription = c.CategoryDescription,
                CategoryImageUrl = c.CategoryImageUrl,
                Products = c.Products?.Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    ProductDescription = p.ProductDescription,
                    ProductImageUrl = p.ProductImageUrl,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    CategoryName = c.CategoryName
                }).ToList() ?? new List<ProductDto>() 
            }).ToList();
        }

        public async Task AddCategoryAsync(CategoryDto categoryDto)
        {
            var category = new Category
            {
                CategoryId = categoryDto.CategoryId,
                CategoryName = categoryDto.CategoryName,
                CategoryDescription = categoryDto.CategoryDescription,
                CategoryImageUrl = categoryDto.CategoryImageUrl
            };

            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateCategoryAsync(CategoryDto categoryDto)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryDto.CategoryId);
            if (category == null) throw new KeyNotFoundException("Category not found.");

            category.CategoryName = categoryDto.CategoryName;
            category.CategoryDescription = categoryDto.CategoryDescription;
            category.CategoryImageUrl = categoryDto.CategoryImageUrl;

            _unitOfWork.Categories.Update(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteCategoryAsync(Guid categoryId)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);
            if (category == null) throw new KeyNotFoundException("Category not found.");

            _unitOfWork.Categories.Delete(category);
            await _unitOfWork.SaveAsync();
        }
    }
}
