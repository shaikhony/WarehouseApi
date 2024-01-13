namespace WarehouseApi.Services
{
    public interface ISupplierService
    {
        Task<IEnumerable<Supplier>> GetAll();
        Task<Supplier> GetByID(int id);
        Task<Supplier> Add(Supplier supplier);
        Supplier Update(Supplier supplier);
        Supplier Delete(Supplier supplier);
    }
}
