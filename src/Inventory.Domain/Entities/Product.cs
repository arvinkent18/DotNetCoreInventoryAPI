
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Domain.Entities
{
    public class Product
    {
        public Product() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

         [Required]
        public required string Name { get; set; }

         [Required]
        public required decimal Price { get; set; }
        public required int Quantity { get; set; }
    }
}