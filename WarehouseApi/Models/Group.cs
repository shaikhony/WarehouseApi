using Microsoft.EntityFrameworkCore;

namespace WarehouseApi.Models
{
    [Index(nameof(GroupName), IsUnique = true)]
    public class Group
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [MaxLength(100)]
        public string GroupName { get; set; }
    }
}
