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
    public class ExportedController : ControllerBase
    {
        private readonly IExportedService _exportedService;
        private readonly IProductService _productService;
        private readonly ApplicationDbContext _context;
        public ExportedController(IExportedService exportedService,IProductService productService, ApplicationDbContext context)
        {
            _exportedService = exportedService;
            _productService = productService;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var exported = await _exportedService.GetAll();
                return Ok(exported);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(ExportedDto dto)
        {
            try
            {
                var exported = new Exported
                {
                    Quantity = dto.Quantity,
                    Date = dto.Date,
                    CustomerId = dto.CustomerId,
                    ProductId = dto.ProductId
                };
                var exist = await _productService.IsExist(dto.ProductId, dto.Quantity);
                if (exist == false) return BadRequest($"The requested quantity is not available {dto.Quantity}");
                else
                {
                    await _exportedService.Add(exported);
                    var product = _context.Products.FirstOrDefault(p => p.Id == dto.ProductId);
                    if (product != null)
                    {
                        product.QuantityAvailble -= dto.Quantity;
                        _context.SaveChanges();
                    }
                    return Ok(exported);
                }
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult>UpdateAsync(int id , ExportedDto dto)
        {
            try
            {
                var exported = await _exportedService.GetByID(id);
                if (exported == null)
                    return NotFound($"No exported was found with ID: {id}");

                exported.Quantity = dto.Quantity;
                exported.Date = dto.Date;
                exported.CustomerId = dto.CustomerId;
                exported.ProductId = dto.ProductId;

                var exist = await _productService.IsExist(dto.ProductId, dto.Quantity);
                if (exist == false) return BadRequest($"The requested quantity is not available {dto.Quantity}");
                else
                {
                    _exportedService.Update(exported);
                    return Ok(exported);
                }
            }
            catch (Exception ex) { return BadRequest( ex.Message); }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var exported = await _exportedService.GetByID(id);
                if (exported == null)
                    return NotFound($"No exported was found with ID: {id}");
                _exportedService.Delete(exported);
                return Ok(exported);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
