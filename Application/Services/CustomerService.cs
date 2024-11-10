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
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public async Task<bool> CreateCustomer(Customer customer)
        {
            if (customer!=null)
            {
                await unitOfWork.Customers.Add(customer);
                var result = unitOfWork.Save();
                if (result>0)
                {
                    return true;

                }
                else
                {
                    return  false;
                }
            }
            return  false;
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            if (id>0)
            {
               var cust=await unitOfWork.Customers.GetById(id);
                if (cust!=null)
                {
                    unitOfWork.Customers.Delete(cust);
                   var result= unitOfWork.Save();
                    if (result>0)
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

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
           var customers= await unitOfWork.Customers.GetAll();
            return customers;
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            if (id>0)
            {
                var customer = await unitOfWork.Customers.GetById(id);
                if (customer!=null)
                {
                    return customer;
                }
                return null;
            }
            return null;
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            if (customer!=null)
            {
             var ExistsCustomer=  await unitOfWork.Customers.GetById(customer.CustomerId);
                if (ExistsCustomer!=null)
                {
                    ExistsCustomer.Name = customer.Name;
                    ExistsCustomer.Email = customer.Email;
                    ExistsCustomer.Phone = customer.Phone;
                    ExistsCustomer.Address = customer.Address;
                    unitOfWork.Customers.Update(ExistsCustomer);
                   var result= unitOfWork.Save();
                    if (result>0)
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
