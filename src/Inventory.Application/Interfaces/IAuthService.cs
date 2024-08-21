using Inventory.Application.DTO;

namespace Inventory.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(LoginUserDto loginDto);
    }
}
