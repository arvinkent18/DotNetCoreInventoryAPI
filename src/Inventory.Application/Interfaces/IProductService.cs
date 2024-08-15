using Inventory.Application.DTO;
using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(CreateProductDto productDto);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
    }
}