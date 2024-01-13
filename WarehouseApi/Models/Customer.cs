namespace WarehouseApi.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [MaxLength (50)]
        public string CustomerName { get; set; }
        [MaxLength(30)]
        public string CustomerPhoneNumber { get; set; }
        [MaxLength(50)]
        public string CustomerEmail { get; set;}
        public Boolean Treat { get; set; }

    }
}
