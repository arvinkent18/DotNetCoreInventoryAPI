using Inventory.Application.DTO;
using Inventory.Application.Exceptions;
using Inventory.Application.Interfaces;
using Inventory.Application.Responses;
using Inventory.Domain.Entities;
using Inventory.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IPagedResult<Product>> GetAllProductsAsync(Guid? userId, GetAllProductsDto getAllProductsDto)
        {
            try
            {
                var query = _context.Products.AsQueryable();

                if (userId.HasValue)
                {
                    query = query.Where(p => p.UserId == userId.Value);
                }

                var totalCount = await query.CountAsync();

                var products = await query
                    .Skip((getAllProductsDto.PageIndex - 1) * getAllProductsDto.PageSize)
                    .Take(getAllProductsDto.PageSize)
                    .ToListAsync();

                return new PagedResult<Product>
                {
                    Items = products,
                    TotalCount = totalCount,
                    PageIndex = getAllProductsDto.PageIndex,
                    PageSize = getAllProductsDto.PageSize
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving products.", ex);
            }
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                throw new ProductNotFoundException();
            }

            return product;
        }

        public async Task<Product> AddProductAsync(Guid userId, CreateProductDto productDto)
        {
            var existingProduct = await _context.Products
        .FirstOrDefaultAsync(p => p.Name == productDto.Name);

            if (existingProduct != null)
            {
                throw new ProductAlreadyExistsException();
            }

            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Quantity = productDto.Quantity,
                UserId = userId,
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task UpdateProductAsync(Guid id, UpdateProductDto productDto)
        {
            var existingProduct = await _context.Products.FindAsync(id);

            if (existingProduct == null)
            {
                throw new ProductNotFoundException();
            }

            if (!string.IsNullOrEmpty(productDto.Name))
            {
                existingProduct.Name = productDto.Name;
            }

            if (productDto.Price.HasValue)
            {
                existingProduct.Price = productDto.Price.Value;
            }

            if (productDto.Quantity.HasValue)
            {
                existingProduct.Quantity = productDto.Quantity.Value;
            }

            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            
            if (product == null)
            {
                throw new ProductNotFoundException();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }


    }
}
