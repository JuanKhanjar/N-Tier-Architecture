using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using N_Tier_Architecture.business.Services.Contracts;
using N_Tier_Architecture.core.Entities;

namespace N_Tier_Architecture.api.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound("Category not found");
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            await _categoryService.AddCategoryAsync(category);
            return CreatedAtAction(nameof(GetAllCategories), new { id = category.CategoryId }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, Category category)
        {
            if (id != category.CategoryId) return BadRequest("Category ID mismatch");

            await _categoryService.UpdateCategoryAsync(category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}
