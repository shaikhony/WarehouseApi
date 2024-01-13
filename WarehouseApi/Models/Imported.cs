namespace WarehouseApi.Models
{
    public class Imported
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }//navigation property

        public int ProductId { get; set; }
        public Product Product { get; set; }//navigation property
    }
}
