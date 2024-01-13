namespace WarehouseApi.Dtos
{
    [Table("RelationProGrops")]
    public class RelationProGropDto
    {
        public int GroupId { get; set; }
        public int ProductId { get; set; }
    }
}
