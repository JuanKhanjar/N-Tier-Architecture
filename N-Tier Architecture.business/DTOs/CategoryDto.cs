namespace N_Tier_Architecture.business.DTOs
{
    public class CategoryDto
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? CategoryDescription { get; set; }
        public string? CategoryImageUrl { get; set; }
        public List<ProductDto>? Products { get; set; }

        public int ProductCount { get; set; }
        public decimal? TotalProductValue { get; set; }
        public decimal? AverageProductPrice { get; set; }
    }
}
/*
 * Ideal Layer for DTOs
DTOs should be placed in a separate folder under the Business Layer, as they represent data contracts between the Service Layer and the API Controllers.
 Why Put DTOs in the Business Layer?
✅ Encapsulation – Prevents exposing the internal domain models directly.
✅ Separation of Concerns – Keeps DTOs separate from Database Entities.
✅ Better API Structure – Allows easy modifications to API responses without affecting the database
 */