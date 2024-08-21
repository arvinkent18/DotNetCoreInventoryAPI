using Inventory.Application.DTO;
using Inventory.Application.Interfaces;
using Inventory.Application.Services;
using Inventory.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [ActionName("GetAll")]
        public IEnumerable<User> GetAllUsers()
        {
            return _userService.GetAllUsers();
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get(Guid id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }


        [HttpPost]
        public IActionResult AddUser([FromBody] CreateUserDto userDto)
        {
            var response = new
            {
                Message = "User created successfully",
            };

            return CreatedAtAction(nameof(Get), new { id = userDto.Email }, response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(Guid id, UpdateUserDto userDto)
        {
            var user = _userService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            _userService.UpdateUser(id, userDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            _userService.DeleteUser(id);
            return NoContent();
        }
    }
}
