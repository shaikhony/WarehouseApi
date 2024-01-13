using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseApi.Dtos;
using WarehouseApi.Services;

namespace WarehouseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        private readonly IImportedService _importedService;
        private readonly ApplicationDbContext _context;
        public SupplierController(ISupplierService supplierService,IImportedService importedService, ApplicationDbContext context)
        {
            _supplierService = supplierService;
            _importedService = importedService;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var suppliers = await _supplierService.GetAll();
                return Ok(suppliers);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
       
        [HttpGet("totalReceivedAmount")]
        public IActionResult GetTotalReceivedAmount()
        {
            var query = from s in _context.Suppliers
                        join i in _context.Importeds on s.Id equals i.SupplierId
                        join p in _context.Products on i.ProductId equals p.Id
                        group new { s, i, p } by new { s.Id, s.SupplierName } into g
                        select new
                        {
                            g.Key.Id,
                            g.Key.SupplierName,
                            TotalReceivedAmount = g.Sum(x => x.i.Quantity * x.p.Price)
                        };

            return Ok(query.ToList());
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(SupplierDto dto)
        {
            try
            {
                var supplier = new Supplier
                {
                    SupplierName = dto.SupplierName,
                    SupplierPhoneNumber = dto.SupplierPhoneNumber,
                    SupplierEmail = dto.SupplierEmail,
                    Treat = dto.Treat
                };
                await _supplierService.Add(supplier);
                return Ok(supplier);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, SupplierDto dto)
        {
            try
            {
                var supplier = await _supplierService.GetByID(id);
                if (supplier == null) return NotFound($"No Supplier was found with ID: {id}");

                supplier.SupplierName = dto.SupplierName;
                supplier.SupplierPhoneNumber = dto.SupplierPhoneNumber;
                supplier.SupplierEmail = dto.SupplierEmail;
                supplier.Treat = dto.Treat;

                _supplierService.Update(supplier);
                return Ok(supplier);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpPut("TreatUpdate {id}")]
        public async Task<IActionResult> TreatUpdate(int id, SupplierTreatDto dto)
        {
            try
            {
                var supplier = await _supplierService.GetByID(id);
                if (supplier == null) return NotFound($"No Supplier was found with ID: {id}");

                supplier.Treat = dto.Treat;
                _supplierService.Update(supplier);
                return Ok(supplier);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var supplier = await _supplierService.GetByID(id);
                if (supplier == null) return NotFound($"No Supplier was found with ID: {id}");

                _supplierService.Delete(supplier);
                return Ok(supplier);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
