using System.ComponentModel.DataAnnotations;

namespace Inventory.Domain.Entities
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid();
            Products = new List<Product>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}