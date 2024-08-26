
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required]
        public required int Quantity { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}