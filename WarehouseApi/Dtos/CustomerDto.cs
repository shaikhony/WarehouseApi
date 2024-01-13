namespace WarehouseApi.Dtos
{
    [Table("Customers")]
    public class CustomerDto
    {
        [MaxLength(50)]
        public string CustomerName { get; set; }
        [MaxLength(30)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "رقم الهاتف يجب أن يحتوي على أرقام فقط.")]
        public string CustomerPhoneNumber { get; set; }
        [MaxLength(50)]
        public string CustomerEmail { get; set; }
        public Boolean Treat { get; set; }
    }
}
