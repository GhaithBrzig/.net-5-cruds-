// ProductsController.cs
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication3.Model;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            return product != null ? Ok(product) : NotFound($"Product with ID {productId} not found.");
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product productRequest)
        {
            if (productRequest == null)
            {
                return BadRequest("Product data is invalid.");
            }

            var addedProduct = await _productService.AddProductAsync(productRequest);
            return Ok(addedProduct);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] Product updatedProduct)
        {
            if (updatedProduct == null)
            {
                return BadRequest("Invalid request body.");
            }

            var updated = await _productService.UpdateProductAsync(productId, updatedProduct);
            return updated != null ? Ok(updated) : NotFound($"Product with ID {productId} not found.");
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            await _productService.DeleteProductAsync(productId);
            return Ok(new { message = $"Product with ID {productId} has been deleted." });
        }

        [HttpGet("filteredAndSorted")]
        public async Task<IActionResult> GetFilteredAndSortedProducts(
            [FromQuery] string filterBy,
            [FromQuery] string sortBy,
            [FromQuery] bool isAscending)
        {
            var products = await _productService.GetFilteredAndSortedProductsAsync(filterBy, sortBy, isAscending);
            return Ok(products);
        }
    }
}
