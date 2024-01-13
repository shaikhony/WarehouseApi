namespace WarehouseApi.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string SupplierName { get; set; }
        [MaxLength(30)]
        public string SupplierPhoneNumber { get; set; }
        [MaxLength(50)]
        public string SupplierEmail { get; set; }
        public Boolean Treat { get; set; }
    }
}
