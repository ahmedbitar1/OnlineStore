using Application.Interfaces;
using Application.Services;
using Core.Interfaces;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ServiceExtention
{
   public static class ServiceExtention
    {
        public static IServiceCollection AddServices(this IServiceCollection service,
                IConfiguration configuration)
        {
            //
            service.AddDbContextPool<DBContextClass>(op => op.UseSqlServer(configuration.GetConnectionString("con")));
            service.AddScoped<IUnitOfWork, UnitOfWork>();
            service.AddScoped<ICustomerRepository, CustomerRepository>();
            service.AddScoped<ICustomerService, CustomerService>();
            service.AddScoped<IProductRepository, ProductRepository>();
            service.AddScoped<IProductService, ProductService>();
            return service;
        }
    }
}
