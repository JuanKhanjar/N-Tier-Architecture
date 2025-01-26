using N_Tier_Architecture.core.Entities;

namespace N_Tier_Architecture.business.Services.Contracts
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(Guid orderId);
        Task PlaceOrderAsync(Order order);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(Guid orderId);
    }


}
