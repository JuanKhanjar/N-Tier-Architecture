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
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _unitOfWork.Customers.GetAllAsync();
        }

        public async Task<Customer?> GetCustomerByIdAsync(Guid customerId)
        {
            return await _unitOfWork.Customers.GetByIdAsync(customerId);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            await _unitOfWork.Customers.AddAsync(customer);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            var existingCustomer = await _unitOfWork.Customers.GetByIdAsync(customer.CustomerId);
            if (existingCustomer == null)
            {
                throw new KeyNotFoundException("Customer not found.");
            }

            existingCustomer.CustomerName = customer.CustomerName;
            existingCustomer.CustomerEmail = customer.CustomerEmail;

            _unitOfWork.Customers.Update(existingCustomer);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteCustomerAsync(Guid customerId)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found.");
            }

            _unitOfWork.Customers.Delete(customer);
            await _unitOfWork.SaveAsync();
        }
    }
}
