namespace WarehouseApi.Dtos
{
    [Table("Products")]
    public class ProductDto
    {
        [MaxLength(50)]
        public string ProductName { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "القية الدنيا يجب أن تحتوي على أرقام فقط.")]
        public int Minimum { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "الكمية المتاحة يجب أن تحتوي على أرقام فقط.")]
        public int QuantityAvailble { get; set; }
        [RegularExpression(@"^\d+(\.\d+)?$", ErrorMessage = "السعر يجب أن يحتوي على أرقام فقط.")]
        public double Price { get; set; }
        public Boolean Effective { get; set; }
        public List<int> GroupsIds { get; set; }
    }
}
