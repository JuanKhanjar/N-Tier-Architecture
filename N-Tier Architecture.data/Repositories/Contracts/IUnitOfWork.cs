using N_Tier_Architecture.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier_Architecture.data.Repositories.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customer> Customers { get; }
        IRepository<Category> Categories { get; }
        IRepository<Product> Products { get; }
        IRepository<Order> Orders { get; }
        IRepository<OrderDetail> OrderDetails { get; }

        Task<int> SaveAsync();
    }
}
