using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N_Tier_Architecture.business.Services.Contracts;
using N_Tier_Architecture.business.Services.Implementaions;
using N_Tier_Architecture.data.Data;
using N_Tier_Architecture.data.Repositories.Contracts;
using N_Tier_Architecture.data.Repositories.Implementaions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Tier_Architecture.root.Dependencies
{
    public class CompositionRoot
    {
        public static void InjectDependencies(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //         options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContextFactory<ApplicationDbContext>(options =>
                     options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICustomerService, CustomerService>();

            //services.AddScoped<IProductService, ProductService>();
            //services.AddScoped<IProductService, ProductService>();
            //services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}

/*
 Option 2: Using Separate DbContext Instances (Scoped to Request)
Instead of using a single DbContext instance, create a new instance per operation.
 */
//builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
