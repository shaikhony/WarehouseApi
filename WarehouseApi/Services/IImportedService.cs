namespace WarehouseApi.Services
{
    public interface IImportedService
    {
        Task<IEnumerable<Imported>> GetAll();
        Task<IEnumerable<Imported>> GetImported(int id);
        Task<Imported> GetByID(int id);
        Task<Imported> Add(Imported imported);
        Imported Update(Imported imported);
        Imported Delete(Imported imported);
    }
}
