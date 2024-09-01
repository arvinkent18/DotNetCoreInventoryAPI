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

        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}