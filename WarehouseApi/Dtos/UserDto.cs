namespace WarehouseApi.Dtos
{
    [Table("Customers")]
    public class UserDto
    {
        [MaxLength(30)]
        public string UserName { get; set; }
        [MaxLength(30)]
        public string Password { get; set; }
        public Boolean IsAdmin { get; set; }
    }
}
