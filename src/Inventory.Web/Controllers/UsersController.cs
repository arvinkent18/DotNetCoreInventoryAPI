﻿using Inventory.Application.DTO;
using Inventory.Application.Interfaces;
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
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserByIdAsync(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


        [HttpPost]
        public async Task<IActionResult> AddUserAsync([FromBody] CreateUserDto userDto)
        {
            var createdUser = await _userService.AddUserAsync(userDto);

            var uri = Url.Action("GetUserByIdAsync", new { id = createdUser.Id });

            return Created(uri, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(Guid id, UpdateUserDto userDto)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

           await _userService.UpdateUserAsync(id, userDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            
            return NoContent();
        }
    }
}
