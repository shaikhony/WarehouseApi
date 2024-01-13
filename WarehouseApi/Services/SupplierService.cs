
using WarehouseApi.Models;

namespace WarehouseApi.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ApplicationDbContext _context;

        public SupplierService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Supplier> Add(Supplier supplier)
        {
            await _context.Suppliers.AddAsync(supplier);
            _context.SaveChanges();
            return supplier;
        }

        public Supplier Delete(Supplier supplier)
        {
            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();
            return supplier;
        }

        public async Task<IEnumerable<Supplier>> GetAll()
        {
            return await _context.Suppliers.OrderBy(s => s.SupplierName).ToListAsync();
        }

        public async Task<Supplier> GetByID(int id)
        {
            return await _context.Suppliers.SingleOrDefaultAsync(s => s.Id == id);
        }
        public Supplier Update(Supplier supplier)
        {
            _context.Suppliers.Update(supplier);
            _context.SaveChanges();
            return supplier;
        }
    }
}
