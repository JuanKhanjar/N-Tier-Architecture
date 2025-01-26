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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _unitOfWork.Categories.GetAllAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid categoryId)
        {
            return await _unitOfWork.Categories.GetByIdAsync(categoryId);
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            var existingCategory = await _unitOfWork.Categories.GetByIdAsync(category.CategoryId);
            if (existingCategory == null) throw new KeyNotFoundException("Category not found");

            existingCategory.CategoryName = category.CategoryName;
            existingCategory.CategoryDescription = category.CategoryDescription;
            existingCategory.CategoryImageUrl = category.CategoryImageUrl;

            _unitOfWork.Categories.Update(existingCategory);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteCategoryAsync(Guid categoryId)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);
            if (category == null) throw new KeyNotFoundException("Category not found");

            _unitOfWork.Categories.Delete(category);
            await _unitOfWork.SaveAsync();
        }
    }
}
