using Inventory.Application.DTO;
using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces;
public interface IProductService
{
    Task<IPagedResult<Product>> GetAllProductsAsync(Guid? id, GetAllProductsDto getAllProductsDto);
    Task<Product> GetProductByIdAsync(Guid id);
    Task<Product> AddProductAsync(Guid userId, CreateProductDto productDto);
    Task UpdateProductAsync(Guid id, UpdateProductDto productDto);
    Task DeleteProductAsync(Guid id);
}