using Inventory.Application.DTO;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Infrastructure;
namespace Inventory.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            try
            {
                return _context.Users.ToList();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving users.", ex);
            }
        }

        public User GetUserById(Guid id)
        {
            var user = _context.Users.Find(id);

            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            return user;
        }

        public void AddUser(CreateUserDto userDto)
        {
            var user = new User
            {
                Email = userDto.Email,
                Password = userDto.Password
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(Guid id, UpdateUserDto userDto)
        {
            var existingUser = _context.Users.Find(id);

            if (existingUser == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            if (!string.IsNullOrEmpty(userDto.Email))
            {
                existingUser.Email = userDto.Email;
            }

            _context.Users.Update(existingUser);
            _context.SaveChanges();
        }

        public void DeleteUser(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("User not found.");
            }
        }
    }
}
