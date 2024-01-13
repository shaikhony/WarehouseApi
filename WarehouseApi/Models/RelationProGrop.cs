namespace WarehouseApi.Models
{
    public class RelationProGrop
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }//navigation property

        public int ProductId { get; set; }
        public Product Product { get; set; }//navigation property
    }
}
