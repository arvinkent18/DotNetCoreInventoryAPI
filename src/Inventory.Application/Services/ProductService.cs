using Inventory.Application.DTO;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Infrastructure;

namespace Inventory.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                return _context.Products.ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving products.", ex);
            }
        }

        public Product GetProductById(Guid id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                throw new InvalidOperationException("Product not found.");
            }

            return product;
        }

        public void AddProduct(CreateProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Quantity = productDto.Quantity
            };

            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Guid id, UpdateProductDto productDto)
        {
            var existingProduct = _context.Products.Find(id);

            if (existingProduct == null)
            {
                throw new InvalidOperationException("Product not found.");
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
            _context.SaveChanges();
        }

        public void DeleteProduct(Guid id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Product not found.");
            }
        }
    }
}
