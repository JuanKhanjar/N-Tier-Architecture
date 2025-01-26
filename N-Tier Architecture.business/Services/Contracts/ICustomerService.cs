using N_Tier_Architecture.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier_Architecture.business.Services.Contracts
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(Guid customerId);
        Task<Customer?> GetCustomerByAuthUserIdAsync(string authUserId);
        Task<Customer?> GetCustomerByAuthUserIdAndEmailAsync(string authUserId, string email);
        Task<Customer?> GetCustomerAsync(ISpecification<Customer> specification);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(Guid customerId);
    }
}


/*
 * Those 3 methods could be :
Task<Customer?> GetCustomerByIdAsync(Guid customerId);
Task<Customer?> GetCustomerByAuthUserIdAsync(string authUserId);
Task<Customer?> GetCustomerByAuthUserIdAndEmailAsync(string authUserId, string email);
This:
 Task<Customer?> GetCustomerAsync(Guid? customerId = null, string? authUserId = null, string? email = null);
 */

/*Implementaion :
 public async Task<Customer?> GetCustomerAsync(Guid? customerId = null, string? authUserId = null, string? email = null)
    {
        if (customerId != null)
        {
            return await _unitOfWork.Customers.GetByIdAsync(customerId.Value);
        }

        if (!string.IsNullOrEmpty(authUserId) && !string.IsNullOrEmpty(email))
        {
            return await _unitOfWork.Customers.FindAsync(c => c.AuthUserId == authUserId && c.CustomerEmail == email);
        }

        if (!string.IsNullOrEmpty(authUserId))
        {
            return await _unitOfWork.Customers.FindAsync(c => c.AuthUserId == authUserId);
        }

        return null; // Or throw an exception if the parameters are invalid
    }
*/

/*Option 3: Use Specification Pattern*/
/*
 * 
 public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
}

public class CustomerSpecification : ISpecification<Customer>
{
    public Expression<Func<Customer, bool>> Criteria { get; }

    public CustomerSpecification(Guid? customerId = null, string? authUserId = null, string? email = null)
    {
        if (customerId != null)
        {
            Criteria = c => c.CustomerId == customerId;
        }
        else if (!string.IsNullOrEmpty(authUserId) && !string.IsNullOrEmpty(email))
        {
            Criteria = c => c.AuthUserId == authUserId && c.CustomerEmail == email;
        }
        else if (!string.IsNullOrEmpty(authUserId))
        {
            Criteria = c => c.AuthUserId == authUserId;
        }
        else
        {
            Criteria = c => true; // Or throw an exception
        }
    }
}

public interface ICustomerService
{
    Task<Customer?> GetCustomerAsync(ISpecification<Customer> specification);
}


public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;

    public CustomerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Customer?> GetCustomerAsync(ISpecification<Customer> specification)
    {
        return await _unitOfWork.Customers.FindAsync(specification.Criteria);
    }
}

var customerByIdSpec = new CustomerSpecification(customerId: customerId);
var customerByAuthUserIdSpec = new CustomerSpecification(authUserId: authUserId);
var customerByAuthUserIdAndEmailSpec = new CustomerSpecification(authUserId: authUserId, email: email);

var customer = await _customerService.GetCustomerAsync(customerByIdSpec);

 */