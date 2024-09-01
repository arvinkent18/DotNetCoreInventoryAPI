
namespace Inventory.Application.DTO;
public class GetAllUsersDto
{
    public required int PageIndex { get; set; } = 1;
    public required int PageSize { get; set; } = 10;
}

