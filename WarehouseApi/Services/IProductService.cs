using WarehouseApi.Models;

namespace WarehouseApi.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAll();
        Task<IEnumerable<Product>> GetEffectiveProducts();
        Task<IEnumerable<Product>> GetMinimumProducts();
        Task<Product> GetById(int id);
        Task<bool> IsExist(int id, int quantity);
        Task<Product> Add(Product product);
        Product Update(Product product);
        Product Delete(Product product);
        void DeleteProductRelations(Product product);

    }
}
