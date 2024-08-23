using Inventory.Application.DTO;
using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(Guid id);
        Task AddProductAsync(Guid userId, CreateProductDto productDto);
        Task UpdateProductAsync(Guid id, UpdateProductDto productDto);
        Task DeleteProductAsync(Guid id);
    }
}