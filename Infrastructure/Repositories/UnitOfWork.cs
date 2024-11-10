using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly DBContextClass context;
        public ICustomerRepository Customers { get; }
        public IProductRepository Products { get; }
        public UnitOfWork(DBContextClass context,ICustomerRepository customerRepository,IProductRepository productRepository)
        {
            this.context = context;
            this.Customers = customerRepository;
            this.Products = productRepository;
        }


        public int Save()
        {
            return context.SaveChanges();
        }


        public void Dispose()
        {
            Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
