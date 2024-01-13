using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseApi.Dtos;
using WarehouseApi.Services;

namespace WarehouseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        
        private readonly IGroupsService _groupsService;

        public GroupsController(IGroupsService groupsService)
        {
            _groupsService = groupsService;
           
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var groups = await _groupsService.GetAll();
                return Ok(groups);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(GroupDto dto)
        {
            try
            {
                var group = new Group
                {
                    Id = dto.Id,
                    GroupName = dto.GroupName
                };
                await _groupsService.Add(group);
                return Ok(group);
            }
            catch (Exception)
            {
                return BadRequest("The use of duplicate values ​​is prohibited");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id , GroupDto dto)
        {
            try
            {
                var group = await _groupsService.GetByID(id);
                if (group == null)
                    return NotFound($"No group was found with ID: {id}");
                group.Id = dto.Id;
                group.GroupName = dto.GroupName;

                _groupsService.Update(group);
                return Ok(group);
            }
            catch (Exception)
            {
                return BadRequest("The use of duplicate values ​​is prohibited");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var group = await _groupsService.GetByID(id);
                if (group == null)
                    return NotFound($"No group was found with ID: {id}");

                _groupsService.Delete(group);
                return Ok(group);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
