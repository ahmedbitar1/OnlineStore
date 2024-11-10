using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository:GenericeRepository<Product>,IProductRepository
    {
        public ProductRepository(DBContextClass context):base(context)
        {
                
        }
    }
}
