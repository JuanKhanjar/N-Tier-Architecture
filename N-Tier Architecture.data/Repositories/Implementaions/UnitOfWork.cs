using Microsoft.EntityFrameworkCore;
using N_Tier_Architecture.core.Entities;
using N_Tier_Architecture.data.Data;
using N_Tier_Architecture.data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier_Architecture.data.Repositories.Implementaions
{
    public class UnitOfWork : IUnitOfWork
    {
        //private readonly ApplicationDbContext _context;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public UnitOfWork(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory; 
            Customers = new CustomerRepository(_contextFactory);
            Products = new ProductRepository(_contextFactory);
            Orders = new OrderRepository(_contextFactory);
            Categories = new CategoryRepository(_contextFactory);
            OrderDetails = new GenericRepository<OrderDetail>(_contextFactory);
            CategorySummaries = new CategorySummaryRepository(_contextFactory);
        }

        public ICustomerRepository Customers { get; private set; }
        public IProductRepository Products { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public IRepository<OrderDetail> OrderDetails { get; private set; }

        public ICategorySummaryRepository CategorySummaries { get; private set; }

        public async Task SaveAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            await context.SaveChangesAsync();
        }
    }

}
