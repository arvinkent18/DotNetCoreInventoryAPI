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

        public void UpdateProduct(Product product)
        {
            var existingProduct = _context.Products.Find(product.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Quantity = product.Quantity;

                _context.Products.Update(existingProduct);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Product not found.");
            }
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
