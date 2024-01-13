namespace WarehouseApi.Dtos
{
    [Table("Importeds")]
    public class ImportedDto
    {
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        public int SupplierId { get; set; }
        public int ProductId { get; set; }
    }
}
