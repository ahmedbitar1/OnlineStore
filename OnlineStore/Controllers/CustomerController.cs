using Application.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;

        public CustomerController(ICustomerService customerService )
        {
            this.customerService = customerService;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public async Task< IActionResult> Get()
        {
         var customers=  await customerService.GetAllCustomers();
            if (customers==null)
            {
                return BadRequest();
            }
            return Ok(customers);
        }


        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public async  Task< IActionResult> Get(int id)
        {
          var customer= await customerService.GetCustomerById(id);
            if (customer == null)
            {
                return BadRequest();    
            }
            return Ok(customer);
        }



        [HttpPost]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create(Customer customer)
        {
            var isCreated = await customerService.CreateCustomer(customer);
            if (isCreated)
            {
                return Ok(isCreated);

            }
            else
            {
                return BadRequest();
            }

        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public async Task< IActionResult> Delete(int id)
        {
          var IsDeleted= await customerService.DeleteCustomer(id);
            if (IsDeleted)
            {
                return Ok(IsDeleted);
            }
            return BadRequest();
        }


        [HttpPut]
        [Route("Update")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public async  Task< IActionResult> UpdateCustomer(Customer cust)
        {
            if (cust!=null)
            {
             var IsUpdated=  await customerService.UpdateCustomer(cust);
                if (IsUpdated)
                {
                    return Ok(IsUpdated);
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }
    }
}
