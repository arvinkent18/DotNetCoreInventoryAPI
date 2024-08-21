using Inventory.Application.DTO;
using Inventory.Domain.Entities;

namespace Inventory.Application.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(Guid id);
        void AddUser(CreateUserDto userDto);
        void UpdateUser(Guid id, UpdateUserDto userDto);
        void DeleteUser(Guid id);
    }
}