namespace WarehouseApi.Dtos
{
    [Table("Suppliers")]
    public class SupplierDto
    {
        [MaxLength(50)]
        public string SupplierName { get; set; }
        [MaxLength(30)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "رقم الهاتف يجب أن يحتوي على أرقام فقط.")]
        public string SupplierPhoneNumber { get; set; }
        [MaxLength(50)]
        public string SupplierEmail { get; set; }
        public Boolean Treat { get; set; }
    }
}
