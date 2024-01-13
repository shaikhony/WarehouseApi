namespace WarehouseApi.Services
{
    public interface IExportedService
    {
        Task<IEnumerable<Exported>> GetAll();
        Task<Exported> GetByID(int id);
        Task<Exported> Add(Exported exported);
        Exported Update(Exported exported);
        Exported Delete(Exported exported);
    }
}
