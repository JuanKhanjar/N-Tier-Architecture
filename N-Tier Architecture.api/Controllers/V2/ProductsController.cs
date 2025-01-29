using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using N_Tier_Architecture.business.Services.Contracts;

namespace N_Tier_Architecture.api.Controllers.V2
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllProducts()
        //{
        //    // Example: Add new fields or adjust response for version 2
        //    var products = await _productService.GetAllProductWithIncludesAsync();
        //    return Ok(products.Select(p => new
        //    {
        //        p.ProductId,
        //        p.ProductName,
        //        p.Price,
        //        ImageUrl = p.ProductImageUrl ?? "default-image.png",
        //        Category = p.Category == null
        //               ? null
        //               : new
        //               {
        //                   p.Category.CategoryId,
        //                   p.Category.CategoryName
        //               }
        //    }));
        //}

        // Other endpoints remain unchanged or adjusted
    }
}
