namespace N_Tier_Architecture.business.DTOs
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string? ProductDescription { get; set; }
        public string? ProductImageUrl { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public string? CategoryName { get; set; } // Optional: Include only the name
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