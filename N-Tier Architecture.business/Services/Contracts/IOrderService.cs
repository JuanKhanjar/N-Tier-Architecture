using N_Tier_Architecture.core.Entities;
using N_Tier_Architecture.data.QueryObjects;

namespace N_Tier_Architecture.business.Services.Contracts
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(Guid orderId);
        Task<IEnumerable<Order>> FindOrdersAsync(OrderQueryParameters parameters);
        Task PlaceOrderAsync(Order order, IEnumerable<OrderDetail> orderDetails);
        Task DeleteOrderAsync(Guid orderId);
    }


}
