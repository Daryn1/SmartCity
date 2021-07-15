using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartCity.Data.Entities.UserAccount;
using SmartCity.Services.Interfaces;
using SmartCity.Services.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartCity.Web.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        // GET: api/products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Get([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            return await productService.GetProductsAsync(pageNumber ?? 1, pageSize ?? 5);
        }

        // GET api/products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(long id)
        {
            var product = await productService.GetProductAsync(id);

            if (product == null)
            {
                return NotFound($"Product with Id = {id} not found");
            }
            
            return product;
        }

        // POST api/products
        [HttpPost]
        public async Task<ActionResult<Product>> PostTransaction(Product product)
        {
            // Exclude property from binding.
            product.Id = 0;

            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(modelStateEntry => modelStateEntry.Errors.Select(b => b.ErrorMessage)).ToList();
                return BadRequest(errorMessages);
            }
            
            await productService.CreateProductAsync(product);

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // PUT api/products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(long id, Product product)
        {
            if (!ModelState.IsValid)
            {
                var errorMessages = ModelState.Values.SelectMany(modelStateEntry => modelStateEntry.Errors.Select(b => b.ErrorMessage)).ToList();
                return BadRequest(errorMessages);
            }

            if (id != product.Id)
            {
                return BadRequest(new List<string>() { "Product Id mismatch" });
            }

            if (!await productService.ProductExistsAsync(id))
            {
                return NotFound(new List<string>() { $"Product with Id = {id} not found" });
            }

            await productService.UpdateProductAsync(product);

            return NoContent();
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await productService.ProductExistsAsync(id))
            {
                return NotFound(new List<string>() { $"Product with Id = {id} not found" });
            }

            await productService.DeleteProductAsync(id);

            return NoContent();
        }
    }
}
