namespace WarehouseApi.Dtos
{
    [Table("Exporteds")]
    public class ExportedDto
    {
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        public int CustomerId { get; set; }
        public int ProductId { get; set; }
    }
}
