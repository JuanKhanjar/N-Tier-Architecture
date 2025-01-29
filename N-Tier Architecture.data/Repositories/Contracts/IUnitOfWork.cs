using N_Tier_Architecture.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier_Architecture.data.Repositories.Contracts
{
    public interface IUnitOfWork 
    {
        ICustomerRepository Customers { get; }
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        ICategoryRepository Categories { get; }
        IRepository<OrderDetail> OrderDetails { get; }
        ICategorySummaryRepository CategorySummaries { get; } // إضافة المخزن الجديد


        Task SaveAsync();
    }
}
