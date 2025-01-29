namespace N_Tier_Architecture.business.DTOs
{
    public class OrderDetailDto
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
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