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
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Customers = new GenericRepository<Customer>(_context);
            Categories = new GenericRepository<Category>(_context);
            Products = new GenericRepository<Product>(_context);
            Orders = new GenericRepository<Order>(_context);
            OrderDetails = new GenericRepository<OrderDetail>(_context);
        }

        public IRepository<Customer> Customers { get; }
        public IRepository<Category> Categories { get; }
        public IRepository<Product> Products { get; }
        public IRepository<Order> Orders { get; }
        public IRepository<OrderDetail> OrderDetails { get; }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
