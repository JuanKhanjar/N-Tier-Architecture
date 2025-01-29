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
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _unitOfWork.Orders.GetAllAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(Guid orderId)
        {
            return await _unitOfWork.Orders.GetByIdAsync(orderId);
        }

        public async Task<IEnumerable<Order>> FindOrdersAsync(OrderQueryParameters parameters)
        {
            return await _unitOfWork.Orders.FindAsync(parameters);
        }

        public async Task PlaceOrderAsync(Order order, IEnumerable<OrderDetail> orderDetails)
        {
            await _unitOfWork.Orders.AddAsync(order);
            foreach (var detail in orderDetails)
            {
                detail.OrderId = order.OrderId;
                await _unitOfWork.OrderDetails.AddAsync(detail);
            }
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteOrderAsync(Guid orderId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null) throw new KeyNotFoundException("Order not found.");

            _unitOfWork.Orders.Delete(order);
            await _unitOfWork.SaveAsync();
        }
    }
}
