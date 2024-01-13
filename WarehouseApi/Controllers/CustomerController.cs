using Microsoft.AspNetCore.Mvc;
using WarehouseApi.Dtos;
using WarehouseApi.MedalWhere;
using WarehouseApi.Services;

namespace WarehouseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ApplicationDbContext _context;

        public CustomerController(ICustomerService customerService, ApplicationDbContext context)
        {
            _customerService = customerService;
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var customers = await _customerService.GetAll();
                return Ok(customers);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
     
        [HttpGet("totalReceivedAmount")]
        public IActionResult GetTotalReceivedAmount()
        {
            var query = from c in _context.Customers
                        join e in _context.Exporteds on c.Id equals e.CustomerId
                        join p in _context.Products on e.ProductId equals p.Id
                        group new { c, e, p } by new { c.Id, c.CustomerName } into g
                        select new
                        {
                            g.Key.Id,
                            g.Key.CustomerName,
                            TotalReceivedAmount = g.Sum(x => x.e.Quantity * x.p.Price)
                        };

            return Ok(query.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CustomerDto dto)
        {
            try
            {
                var customer = new Customer
                {
                    CustomerName = dto.CustomerName,
                    CustomerPhoneNumber = dto.CustomerPhoneNumber,
                    CustomerEmail = dto.CustomerEmail,
                    Treat = dto.Treat
                };
                //if (int.TryParse(dto.CustomerName, out _)) return BadRequest("CustomerName should not be a number.");
                await _customerService.Add(customer);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, CustomerDto dto)
        {
            try
            {
                var customer = await _customerService.GetByID(id);
                if (customer == null) return NotFound($"No Customer was found with ID: {id}");

                customer.CustomerName = dto.CustomerName;
                customer.CustomerPhoneNumber = dto.CustomerPhoneNumber;
                customer.CustomerEmail = dto.CustomerEmail;
                customer.Treat = dto.Treat;

                _customerService.Update(customer);
                return Ok(customer);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpPut("TreatUpdate {id}")]
        public async Task<IActionResult> TreatUpdate(int id, CustomerTreatDto dto)
        {
            try
            {
                var customer = await _customerService.GetByID(id);
                if (customer == null) return BadRequest($"No Customer was found with ID: {id}");

                customer.Treat = dto.Treat;
                _customerService.Update(customer);
                return Ok(customer);
            }
            catch(Exception ex)
            {
                //return BadRequest();
                throw ex;
            }
            }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var customer = await _customerService.GetByID(id);
                if (customer == null) return NotFound($"No Customer was found with ID: {id}");

                _customerService.Delete(customer);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
