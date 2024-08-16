
using System.ComponentModel.DataAnnotations;

namespace Inventory.Domain.Entities
{
    public class Product
    {
        public Product() 
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

         [Required]
        public required string Name { get; set; }

         [Required]
        public required decimal Price { get; set; }
        public required int Quantity { get; set; }
    }
}