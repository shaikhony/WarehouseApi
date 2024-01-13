
using Microsoft.AspNetCore.Mvc;

namespace WarehouseApi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> Add(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            _context.SaveChanges();
            return customer;
        }

        public Customer Delete([FromHeader]Customer customer)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.OrderBy(c => c.CustomerName).ToListAsync();
        }

        public async Task<Customer> GetByID(int id)
        {
            return await _context.Customers.SingleOrDefaultAsync(c => c.Id == id);
        }

        public Customer Update(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
            return customer;
        }
    }
}
