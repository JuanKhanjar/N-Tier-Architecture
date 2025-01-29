using Microsoft.EntityFrameworkCore;
using N_Tier_Architecture.data.Data;
using N_Tier_Architecture.data.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier_Architecture.data.Repositories.Implementaions
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        //protected readonly ApplicationDbContext _context;
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;
        //protected readonly DbSet<T> _dbSet;
        public GenericRepository(/*ApplicationDbContext context*/IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            //_context = context;
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            using var context = _contextFactory.CreateDbContext();
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync(); // Ensure changes are saved immediately
        }

        public void Update(T entity)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Set<T>().Update(entity);
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            using var context = _contextFactory.CreateDbContext();
            context.Set<T>().Remove(entity);
            context.SaveChanges();
        }

        public async Task<IEnumerable<T>> GetAllWithIncludesAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            using var context = _contextFactory.CreateDbContext();
            IQueryable<T> query = context.Set<T>();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.ToListAsync();
        }
    }
}
