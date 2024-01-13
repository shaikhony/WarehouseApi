namespace WarehouseApi.Dtos
{
    [Table("Groups")]
    public class GroupDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [MaxLength(100)]
        [RegularExpression("^[a-z][A-Z]*$", ErrorMessage = "الاسم  يجب أن يحتوي على احرف فقط.")]
        public string GroupName { get; set; }
    }
}
