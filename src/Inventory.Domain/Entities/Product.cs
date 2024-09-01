
using System.ComponentModel.DataAnnotations;

namespace Inventory.Domain.Entities
{
    public class Product
    {
        public Product()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid UserId { get; set; }
    }
}