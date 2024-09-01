using Inventory.Application.DTO;
using Inventory.Application.Exceptions;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Inventory.Web.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [ActionName("GetAll")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsAsync([FromQuery] GetAllProductsDto getAllProductsDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userId, out var parsedUserId))
            {
                return BadRequest("Invalid user ID.");
            }

            var products = await _productService.GetAllProductsAsync(parsedUserId, getAllProductsDto);

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductByIdAsync(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                throw new ProductNotFoundException();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromBody] CreateProductDto productDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(userId, out var parsedUserId))
            {
                return BadRequest("Invalid user ID.");
            }

            var createdProduct = await _productService.AddProductAsync(parsedUserId, productDto);

            var uri = Url.Action("GetProductByIdAsync", new { id = createdProduct.Id });

            return Created(uri, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductAsync(Guid id, UpdateProductDto productDto)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                throw new ProductNotFoundException();
            }

            await _productService.UpdateProductAsync(id, productDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                throw new ProductNotFoundException();
            }

            await _productService.DeleteProductAsync(product.Id);

            return NoContent();
        }
    }
}
