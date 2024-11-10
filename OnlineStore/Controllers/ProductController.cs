using Application.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await productService.GetAllProduct();
            if (products == null)
            {
                return BadRequest();
            }
            return Ok(products);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await productService.GetProductById(id);
            if (product == null)
            {
                return BadRequest();
            }
            return Ok(product);
        }



        [HttpPost]
        public async Task<IActionResult> Create(Product  product)
        {
            var isCreated = await productService.CreateProduct(product);
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
        public async Task<IActionResult> Delete(int id)
        {
            var IsDeleted = await productService.DeleteProduct(id);
            if (IsDeleted)
            {
                return Ok(IsDeleted);
            }
            return BadRequest();
        }


        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateProduct(Product prod)
        {
            if (prod != null)
            {
                var IsUpdated = await productService.UpdateProduct(prod);
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
