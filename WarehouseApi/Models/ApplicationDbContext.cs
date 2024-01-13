using Microsoft.EntityFrameworkCore;

namespace WarehouseApi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<RelationProGrop> RelationProGrops { get; set; }
        public DbSet<Imported> Importeds { get; set; }
        public DbSet<Exported> Exporteds { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
