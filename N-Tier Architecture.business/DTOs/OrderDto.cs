namespace N_Tier_Architecture.business.DTOs
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public List<OrderDetailDto>? OrderDetails { get; set; }
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