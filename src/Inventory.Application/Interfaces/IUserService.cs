using Inventory.Application.DTO;
using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> AddUserAsync(CreateUserDto userDto);
        Task UpdateUserAsync(Guid id, UpdateUserDto userDto);
        Task DeleteUserAsync(Guid id);
    }
}