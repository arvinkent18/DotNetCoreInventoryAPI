using Inventory.Application.DTO;
using Inventory.Application.Exceptions;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Infrastructure;
using Microsoft.EntityFrameworkCore;
namespace Inventory.Application.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(ApplicationDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while retrieving users.", ex);
            }
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            return user;
        }

        public async Task<User> AddUserAsync(CreateUserDto userDto)
        {
            var existingUser = await _context.Users
       .FirstOrDefaultAsync(p => p.Email == userDto.Email);

            if (existingUser != null)
            {
                throw new UserAlreadyExistsException();
            }

            var hashedPassword = _passwordHasher.HashPassword(userDto.Password);

            var user = new User
            {
                Email = userDto.Email,
                Password = hashedPassword
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task UpdateUserAsync(Guid id, UpdateUserDto userDto)
        {
            var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);

            if (existingUser == null)
            {
                throw new UserNotFoundException();
            }

            if (!string.IsNullOrWhiteSpace(userDto.Email))
            {
                existingUser.Email = userDto.Email;
            }

            if (!string.IsNullOrWhiteSpace(userDto.Password))
            {
                var hashedPassword = _passwordHasher.HashPassword(userDto.Password);
                existingUser.Password = hashedPassword;
            }

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                throw new UserNotFoundException();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
