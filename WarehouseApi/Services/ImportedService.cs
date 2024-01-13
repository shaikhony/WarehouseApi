
namespace WarehouseApi.Services
{
    public class ImportedService : IImportedService
    {
        private readonly ApplicationDbContext _context;

        public ImportedService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Imported> Add(Imported imported)
        {
            await _context.AddAsync(imported);
            _context.SaveChanges();
            return imported;
        }

        public Imported Delete(Imported imported)
        {
            _context.Importeds.Remove(imported);
            _context.SaveChanges();
            return imported;
        }

        public async Task<IEnumerable<Imported>> GetAll()
        {
            return await _context.Importeds
                .Include(s => s.Supplier)
                .Include(p => p.Product)
                .ToListAsync();
        }

        public async Task<Imported> GetByID(int id)
        {
            return await _context.Importeds.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Imported>> GetImported(int id)
        {
            return await _context.Importeds
                .Where(x => x.SupplierId == id)
                .Include(p => p.Product)
                .Include(s => s.Supplier)
                .ToListAsync();
        }

        public Imported Update(Imported imported)
        {
            _context.Importeds.Update(imported);
            _context.SaveChanges();
            return imported;
        }
    }
}
