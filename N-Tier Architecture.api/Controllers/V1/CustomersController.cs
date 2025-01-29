using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using N_Tier_Architecture.business.Services.Contracts;
using N_Tier_Architecture.core.Entities;
using N_Tier_Architecture.data.QueryObjects;

namespace N_Tier_Architecture.api.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        // GET: api/Customer/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(Guid id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound("Customer not found.");
            return Ok(customer);
        }

        // POST: api/Customer
        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _customerService.AddCustomerAsync(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, customer);
        }

        // PUT: api/Customer/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] Customer customer)
        {
            if (id != customer.CustomerId) return BadRequest("Customer ID mismatch.");
            await _customerService.UpdateCustomerAsync(customer);
            return NoContent();
        }

        // DELETE: api/Customer/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }

        // GET: api/Customer/search
        [HttpGet("search")]
        public async Task<IActionResult> SearchCustomers([FromQuery] CustomerQueryParameters parameters)
        {
            var customers = await _customerService.FindCustomersAsync(parameters);
            return Ok(customers);
        }
    }
}
