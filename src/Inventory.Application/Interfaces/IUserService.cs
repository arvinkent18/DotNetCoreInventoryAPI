using Inventory.Application.DTO;

namespace Inventory.Application.Interfaces
{
    public interface IUserService
    {
        Task<string> RegisterUserAsync(RegisterUserDto registerDto);
    }
}