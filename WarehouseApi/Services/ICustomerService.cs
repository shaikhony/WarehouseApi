namespace WarehouseApi.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAll();
        Task<Customer> GetByID(int id);
        Task<Customer> Add(Customer customer);
        Customer Update(Customer customer);
        Customer Delete(Customer customer);

    }
}
