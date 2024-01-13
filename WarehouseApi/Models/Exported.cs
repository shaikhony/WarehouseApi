namespace WarehouseApi.Models
{
    public class Exported
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }//navigation property
        public int ProductId { get; set; }
        public Product Product { get; set; }//navigation property
    }
}
