namespace Inventory.Application.DTO
{
    public class UpdateProductDto
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
    }
}