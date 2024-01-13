namespace WarehouseApi.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetByID(int id);
        Task<User> Add(User user);
        User Update(User user);
        User Delete(User user);
    }
}
