namespace WarehouseApi.Services
{
    public class ExportedService : IExportedService
    {
        private readonly ApplicationDbContext _context;

        public ExportedService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Exported> Add(Exported exported)
        {
            await _context.AddAsync(exported);
            _context.SaveChanges();
            return exported;
        }

        public Exported Delete(Exported exported)
        {
            _context.Exporteds.Remove(exported);
            _context.SaveChanges();
            return exported;
        }

        public async Task<IEnumerable<Exported>> GetAll()
        {
            return await _context.Exporteds
                .Include(c => c.Customer)
                .Include(p => p.Product)
                .ToListAsync();
        }

        public async Task<Exported> GetByID(int id)
        {
            return await _context.Exporteds.SingleOrDefaultAsync(x => x.Id == id);
        }

        public Exported Update(Exported exported)
        {
            _context.Exporteds.Update(exported);
            _context.SaveChanges();
            return exported;
        }
    }
}
