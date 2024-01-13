using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseApi.Dtos;
using WarehouseApi.Models;
using WarehouseApi.Services;

namespace WarehouseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var users = await _userService.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserDto dto)
        {
            try
            {
                var user = new User
                {
                    UserName = dto.UserName,
                    Password = dto.Password,
                    IsAdmin = dto.IsAdmin,
                };
                await _userService.Add(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id , UserDto dto)
        {
            try
            {
                var user = await _userService.GetByID(id);
                if (user == null) return NotFound($"No User was found with ID: {id}");

                user.UserName = dto.UserName;
                user.Password = dto.Password;
                user.IsAdmin = dto.IsAdmin;

                _userService.Update(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var user = await _userService.GetByID(id);
                if (user == null) return NotFound($"No User was found with ID: {id}");

                _userService.Delete(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
