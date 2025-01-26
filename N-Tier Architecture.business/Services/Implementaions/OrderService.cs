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

        public async Task AddOrderAsync(Order order)
        {
            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            var existingOrder = await _unitOfWork.Orders.GetByIdAsync(order.OrderId);
            if (existingOrder == null) throw new KeyNotFoundException("Order not found");

            existingOrder.OrderDate = order.OrderDate;
            existingOrder.CustomerId = order.CustomerId;

            _unitOfWork.Orders.Update(existingOrder);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteOrderAsync(Guid orderId)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
            if (order == null) throw new KeyNotFoundException("Order not found");

            _unitOfWork.Orders.Delete(order);
            await _unitOfWork.SaveAsync();
        }

        public async Task PlaceOrderAsync(Order order)
        {
            // Validate the customer exists
            var customer = await _unitOfWork.Customers.GetByIdAsync(order.CustomerId);
            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found.");
            }

            // Validate and calculate totals for each OrderDetail
            foreach (var detail in order.OrderDetails)
            {
                var product = await _unitOfWork.Products.GetByIdAsync(detail.ProductId);
                if (product == null)
                {
                    throw new KeyNotFoundException($"Product with ID {detail.ProductId} not found.");
                }

                detail.Total = detail.Quantity * product.Price;
            }

            // Add the order and its details to the database
            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.SaveAsync();
        }
    }
}
