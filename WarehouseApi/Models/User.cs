namespace WarehouseApi.Models
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string UserName { get; set; }
        [MaxLength(30)]
        public string Password { get; set; }
        public Boolean IsAdmin { get; set; }
    }
}
