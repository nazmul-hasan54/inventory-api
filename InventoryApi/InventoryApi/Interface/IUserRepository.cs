using InventoryApi.Entities;

namespace InventoryApi.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetByUsernameAsync(string username);
        Task<User> CreateAsync(User user);
    }
}
