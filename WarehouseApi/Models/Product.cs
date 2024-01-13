using Microsoft.EntityFrameworkCore;
namespace WarehouseApi.Models
{
    [Index(nameof(ProductName),IsUnique =true)]
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public  string ProductName { get; set; }
        public int Minimum { get; set; }
        public int QuantityAvailble { get; internal set; }
        public double Price { get; set; }
        public Boolean Effective { get; set; }
        public List<RelationProGrop> RelationProGrops { get; set; }
    }
}
