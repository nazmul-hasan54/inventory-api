using InventoryApi.Entities;

namespace InventoryApi.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> AddAsync(Product p);
        Task UpdateAsync(Product p);
        Task DeleteAsync(int id);
    }
}
