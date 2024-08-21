namespace Inventory.Application.DTO
{
    public class CreateUserDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
