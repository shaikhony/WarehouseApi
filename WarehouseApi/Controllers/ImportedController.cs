using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseApi.Dtos;
using WarehouseApi.Models;
using WarehouseApi.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WarehouseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportedController : ControllerBase
    {
        private readonly IImportedService _importedService;
        private readonly ApplicationDbContext _context;

        public ImportedController(IImportedService importedService, ApplicationDbContext context)
        {
            _importedService = importedService;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var imported = await _importedService.GetAll();
                return Ok(imported);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(ImportedDto dto)
        {
            try
            {
                var imported = new Imported
                {
                    Quantity = dto.Quantity,
                    Date = dto.Date,
                    SupplierId = dto.SupplierId,
                    ProductId = dto.ProductId
                };
                await _importedService.Add(imported);
                var product = _context.Products.FirstOrDefault(p => p.Id == dto.ProductId);
                if (product != null)
                {
                    product.QuantityAvailble += dto.Quantity;
                    _context.SaveChanges();
                }
                return Ok(imported);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, ImportedDto dto)
        {
            try
            {
                var imported = await _importedService.GetByID(id);
                if (imported == null) return NotFound($"No imported was found with ID: {id}");
                imported.Quantity = dto.Quantity;
                imported.Date = dto.Date;
                imported.ProductId = dto.ProductId;
                imported.SupplierId = dto.SupplierId;

                _importedService.Update(imported);
                return Ok(imported);
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
                var imported = await _importedService.GetByID(id);
                if (imported == null) return NotFound($"No imported was found with ID: {id}");

                _importedService.Delete(imported);
                return Ok(imported);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
