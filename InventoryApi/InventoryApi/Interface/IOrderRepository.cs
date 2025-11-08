using InventoryApi.Entities;

namespace InventoryApi.Interface
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task<Order> AddAsync(Order o);
    }
}
