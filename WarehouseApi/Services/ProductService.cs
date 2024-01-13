
namespace WarehouseApi.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Product> Add(Product product)
        {
            await _context.AddAsync(product);
            _context.SaveChanges();
            return product;
        }

        public Product Delete(Product product)
        {
            _context.Remove(product);
            _context.SaveChanges();
            return product;
        }

        public void DeleteProductRelations(Product product)
        {
            _context.RelationProGrops.RemoveRange(product.RelationProGrops);
            _context.SaveChanges();
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products
                .OrderBy(p => p.ProductName)
                .Include(r => r.RelationProGrops)
                .ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.Include(r=>r.RelationProGrops).SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetEffectiveProducts()
        {
            return await _context.Products
                .Where(p => p.Effective == true)
                .Include(r => r.RelationProGrops)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetMinimumProducts()
        {
            return await _context.Products
                .Where(p => p.QuantityAvailble <= p.Minimum)
                .Include(r => r.RelationProGrops)
                .ToListAsync();
        }

        public Task<bool> IsExist(int id, int quantity)
        {
            return _context.Products.AnyAsync(e => e.Id == id && e.QuantityAvailble >= quantity);
        }

        public Product Update(Product product)
        {
            _context.Update(product);
            _context.SaveChanges();
            return product;
        }
    }
}
