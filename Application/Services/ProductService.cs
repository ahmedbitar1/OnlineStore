using Application.Interfaces;
using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }



        public async Task<bool> CreateProduct(Product product)
        {
            if (product != null)
            {
                await unitOfWork.Products.Add(product);
                var result = unitOfWork.Save();
                if (result > 0)
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            if (id > 0)
            {
                var prod = await unitOfWork.Products.GetById(id);
                if (prod != null)
                {
                    unitOfWork.Products.Delete(prod);
                    var result = unitOfWork.Save();
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            var products = await unitOfWork.Products.GetAll();
            return products;
        }

        public  async Task<Product> GetProductById(int id)
        {
            if (id > 0)
            {
                var product = await unitOfWork.Products.GetById(id);
                if (product != null)
                {
                    return product;
                }
                return null;
            }
            return null;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            if (product != null)
            {
                var ExistsProduct = await unitOfWork.Products.GetById(product.Id);
                if (ExistsProduct != null)
                {
                    ExistsProduct.Name = product.Name;
                    ExistsProduct.Price = product.Price;
                  
                    unitOfWork.Products.Update(ExistsProduct);
                    var result = unitOfWork.Save();
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
    }
}
