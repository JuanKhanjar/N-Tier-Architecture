using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using N_Tier_Architecture.business.Services.Contracts;
using N_Tier_Architecture.core.Entities;
using N_Tier_Architecture.data.QueryObjects;

namespace N_Tier_Architecture.api.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/Order
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        // GET: api/Order/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound("Order not found.");
            return Ok(order);
        }

        // POST: api/Order
        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _orderService.PlaceOrderAsync(order, order.OrderDetails);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.OrderId }, order);
        }

        // DELETE: api/Order/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }

        // GET: api/Order/search
        [HttpGet("search")]
        public async Task<IActionResult> SearchOrders([FromQuery] OrderQueryParameters parameters)
        {
            var orders = await _orderService.FindOrdersAsync(parameters);
            return Ok(orders);
        }
    }
}
